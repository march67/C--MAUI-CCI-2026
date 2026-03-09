using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Morpion.Application.GameUseCases;
using Morpion.Application.PlayerUseCases;
using Morpion.Domain.Entities;
using Morpion.Infrastructure.Persistance;
using Morpion.Infrastructure.Persistance.Repositories;

namespace Morpion
{
    public class GameManager
    {
        private readonly PlayerRegisterUseCase _playerRegisterUseCase;
        private readonly GameStartUseCase _gameStartUseCase;
        private readonly IConsoleWrapper _console;
        private Game _currentGame;
        private bool IsFirstTurnOfTheGame = true;
        private Player Player1;
        private Player Player2;
        private IPlayerManager _currentPlayerManagerToPlay;
        public List<IPlayerManager> PlayerList = new List<IPlayerManager>();
        bool Restart = false;

        public GameManager(IConsoleWrapper console, PlayerRegisterUseCase playerRegisterUseCase, GameStartUseCase gameStartUseCase)
        {
            _console = console;
            _playerRegisterUseCase = playerRegisterUseCase;
            _gameStartUseCase = gameStartUseCase;
        }

        public async Task StartGame()
        {
            await ChooseTypeOfGame();

            Console.Clear();

            Board board = new Board();

            RandomizePlayerTurn(PlayerList);

            board.DisplayBoard();

            while (!board.CheckWinCondition(_currentPlayerManagerToPlay.GetPlayerName()) && !board.CheckDraw())
            {
                ChangePlayerTurn();
                board.InputMoveOnBoard(await _currentPlayerManagerToPlay.PlayerInput(board));
                board.DisplayBoard();
            }

            await RecordGameResult(board);

            GameEnded();
        }

        private async Task ChooseTypeOfGame()
        {
            PlayerList.Clear();
            string input;
            do
            {
                Console.Write("Choisissez '1' pour jouer contre un autre humain, '2' pour jouer contre un bot\n");
                input = Console.ReadLine();


            } while (input != "1" && input != "2");

            if (input == "1")
            {
                await InitializeTwoHumanPlayers();
            }
            else if (input == "2")
            {
                await InitializeHumanVsBotPlayers();
            }

        }

        public async Task InitializeTwoHumanPlayers()
        {
            string? input;

            _console.Write("Joueur 1, veuillez saisir votre prénom\n");
            do
            {
               input = _console.ReadLine();
            } while (string.IsNullOrEmpty(input));
            string playerName_1 = input;
            
            Player1 = Player.Create(playerName_1, true);
            Player1 = await _playerRegisterUseCase.Save(Player1);

            _console.Write("Joueur 1, veuillez saisir votre symbole de jeu\n");
            do
            {
                input = _console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            char playerSymbol_1 = input[0];

            _console.Write("Joueur 2, veuillez saisir votre prénom\n");
            do
            {
                input = _console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            string playerName_2 = input;
            
            Player2 = Player.Create(playerName_2, true);
            Player2 = await _playerRegisterUseCase.Save(Player2);

            _console.Write("Joueur 2, veuillez saisir votre symbole de jeu\n");
            do
            {
                input = _console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            char playerSymbol_2 = input[0];

            PlayerList.Add(new HumanPlayerManager { HumanName = playerName_1, HumanSymbol = playerSymbol_1 });
            PlayerList.Add(new HumanPlayerManager { HumanName = playerName_2, HumanSymbol = playerSymbol_2 });
            
            _currentGame = Game.Create(Player1, Player2);
            _currentGame = await _gameStartUseCase.CreateGame(_currentGame);
        }

        public async Task InitializeHumanVsBotPlayers()
        {
            string? input;
            
            _console.Write("Joueur 1, veuillez saisir votre prénom\n");
            do
            {
                input = _console.ReadLine();
            } while (string.IsNullOrEmpty(input));
            string playerName_1 = input;
            
            Player1 = Player.Create(playerName_1, true);
            Player1 = await _playerRegisterUseCase.Save(Player1);

            _console.Write("Joueur 1, veuillez saisir votre symbole de jeu\n");
            do
            {
                input = _console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            char playerSymbol_1 = input[0];

            Random random = new Random();
            string[] botNames = { "Skynet", "HAL-9000", "Cortana", "GLaDOS", "R2D2", "Wall-E" };
            char[] availableSymbols = { 'X', 'O', '#', '@', '*', '&' };

            string botName = botNames[random.Next(botNames.Length)]; // Next génère un entier aléatoire compris entre 0 et la maxValue en argument
            char botSymbol = availableSymbols[random.Next(availableSymbols.Length)];
            
            Player2 = Player.Create(botName, false);
            Player2 = await _playerRegisterUseCase.Save(Player2);

            PlayerList.Add(new HumanPlayerManager { HumanName = playerName_1, HumanSymbol = playerSymbol_1 });
            PlayerList.Add(new BotPlayerManager { BotName = botName, BotSymbol  = botSymbol });
            
            _currentGame = Game.Create(Player1, Player2);
            _currentGame = await _gameStartUseCase.CreateGame(_currentGame);
        }

        public void GameEnded()
        {
            
            Console.Write("\nRejouer une nouvelle partie ? O pour oui, N pour non\n");

            string? input;
            do
            {
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));

            Restart = input[0] == 'O' ? true : false;
            if (Restart)
            {
                Console.Clear();
                StartGame();
            }
            else
            {
                Environment.Exit(0); // Quit application
            }
        }

        private void ChangePlayerTurn()
        {
            if (!IsFirstTurnOfTheGame)
            {
                _currentPlayerManagerToPlay = PlayerList[0] == _currentPlayerManagerToPlay
                    ? PlayerList[1]
                    : PlayerList[0];
            }
            else
            {
                IsFirstTurnOfTheGame = false;
            }
        }

        private void RandomizePlayerTurn(List<IPlayerManager> playerList)
        {
            Random random = new Random();
            int index = random.Next(playerList.Count);
            _currentPlayerManagerToPlay = playerList[index];
        }
        
        private async Task RecordGameResult(Board board)
        {
            if (board.HasWinner())
            {
                var winnerName = _currentPlayerManagerToPlay.GetPlayerName();

                Player winnerEntity, loserEntity;
        
                if (Player1.Name == winnerName)
                {
                    winnerEntity = Player1;
                    loserEntity = Player2;
                }
                else
                {
                    winnerEntity = Player2;
                    loserEntity = Player1;
                }
        
                _currentGame.SetResult(winnerEntity, loserEntity);
                await _gameStartUseCase.UpdateGame(_currentGame);
            }
        }
    }
}
