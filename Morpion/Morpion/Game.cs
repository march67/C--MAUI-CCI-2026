using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    public class Game
    {
        private readonly IConsoleWrapper _console;
        private bool IsFirstTurnOfTheGame = true;
        private IPlayer CurrentPlayerToPlay;
        public List<IPlayer> PlayerList = new List<IPlayer>();
        bool Restart = false;

        public Game()
        {

        }

        public Game(IConsoleWrapper console)
        {
            _console = console;
        }

        public async Task StartGame()
        {
            ChooseTypeOfGame();

            Console.Clear();

            Board board = new Board();

            RandomizePlayerTurn(PlayerList);

            board.DisplayBoard();

            while (!board.CheckWinCondition(CurrentPlayerToPlay.GetPlayerName()) && !board.CheckEndGame())
            {
                ChangePlayerTurn();
                board.InputMoveOnBoard(await CurrentPlayerToPlay.PlayerInput(board));
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

            _console.Write("Joueur 2, veuillez saisir votre symbole de jeu\n");
            do
            {
                input = _console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            char playerSymbol_2 = input[0];

            PlayerList.Add(new HumanPlayer { HumanName = playerName_1, HumanSymbol = playerSymbol_1 });
            PlayerList.Add(new HumanPlayer { HumanName = playerName_2, HumanSymbol = playerSymbol_2 });
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

            PlayerList.Add(new HumanPlayer { HumanName = playerName_1, HumanSymbol = playerSymbol_1 });
            PlayerList.Add(new BotPlayer { BotName = botName, BotSymbol  = botSymbol });
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
                CurrentPlayerToPlay = PlayerList[0] == CurrentPlayerToPlay
                    ? PlayerList[1]
                    : PlayerList[0];
            }
            else
            {
                IsFirstTurnOfTheGame = false;
            }
        }

        private void RandomizePlayerTurn(List<IPlayer> playerList)
        {
            Random random = new Random();
            int index = random.Next(playerList.Count);
            CurrentPlayerToPlay = playerList[index];
        }
    }
}
