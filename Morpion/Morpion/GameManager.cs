using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Morpion.Application.PlayerUseCases;
using Morpion.Domain.Entities;
using Morpion.Infrastructure.Persistance;
using Morpion.Infrastructure.Persistance.Repositories;

namespace Morpion
{
    public class GameManager
    {
        private readonly PlayerRegisterUseCase _playerRegisterUseCase;
        private readonly IConsoleWrapper _console;
        private bool IsFirstTurnOfTheGame = true;
        private IPlayerManager _currentPlayerManagerToPlay;
        public List<IPlayerManager> PlayerList = new List<IPlayerManager>();
        bool Restart = false;

        public GameManager(IConsoleWrapper console, PlayerRegisterUseCase playerRegisterUseCase)
        {
            _console = console;
            _playerRegisterUseCase = playerRegisterUseCase;
        }

        public async Task StartGame()
        {
            ChooseTypeOfGame();

            Console.Clear();

            Board board = new Board();

            RandomizePlayerTurn(PlayerList);

            board.DisplayBoard();

            while (!board.CheckWinCondition(_currentPlayerManagerToPlay.GetPlayerName()) && !board.CheckEndGame())
            {
                ChangePlayerTurn();
                board.InputMoveOnBoard(await _currentPlayerManagerToPlay.PlayerInput(board));
                board.DisplayBoard();
            }

            GameEnded();
        }

        private void ChooseTypeOfGame()
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
                InitializeTwoHumanPlayers();
            }
            else if (input == "2")
            {
                InitializeHumanVsBotPlayers();
            }

        }

        public void InitializeTwoHumanPlayers()
        {
            string? input;

            _console.Write("Joueur 1, veuillez saisir votre prénom\n");
            do
            {
               input = _console.ReadLine();
            } while (string.IsNullOrEmpty(input));
            string playerName_1 = input;
            
            var player_1 = Player.Create(playerName_1, true);
            _playerRegisterUseCase.Save(player_1);

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
            
            var player_2 = Player.Create(playerName_2, true);
            _playerRegisterUseCase.Save(player_2);

            _console.Write("Joueur 2, veuillez saisir votre symbole de jeu\n");
            do
            {
                input = _console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            char playerSymbol_2 = input[0];

            PlayerList.Add(new HumanPlayerManager { HumanName = playerName_1, HumanSymbol = playerSymbol_1 });
            PlayerList.Add(new HumanPlayerManager { HumanName = playerName_2, HumanSymbol = playerSymbol_2 });
        }

        public void InitializeHumanVsBotPlayers()
        {
            string? input;
            
            _console.Write("Joueur 1, veuillez saisir votre prénom\n");
            do
            {
                input = _console.ReadLine();
            } while (string.IsNullOrEmpty(input));
            string playerName_1 = input;
            
            var player_1 = Player.Create(playerName_1, true);
            _playerRegisterUseCase.Save(player_1);

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
            
            var player_2 = Player.Create(botName, false);
            _playerRegisterUseCase.Save(player_2);

            PlayerList.Add(new HumanPlayerManager { HumanName = playerName_1, HumanSymbol = playerSymbol_1 });
            PlayerList.Add(new BotPlayerManager { BotName = botName, BotSymbol  = botSymbol });
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
    }
}
