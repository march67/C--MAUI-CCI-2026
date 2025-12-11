using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    public class Game
    {
        List<Player> playerList = new List<Player>();
        bool Restart = false;

        public void StartGame()
        {
            InitializeTwoPlayers();

            Console.Clear();

            Board board = new Board(playerList);

            GameEnded();
        }

        public void InitializeTwoPlayers()
        {
            string? input;


            Console.Write("Joueur 1, veuillez saisir votre prénom\n");
            do
            {
               input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));
            string playerName_1 = input;

            Console.Write("Joueur 1, veuillez saisir votre symbole de jeu\n");
            do
            {
                input = Console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            char playerSymbol_1 = input[0];

            Console.Write("Joueur 2, veuillez saisir votre prénom\n");
            do
            {
                input = Console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            string playerName_2 = input;

            Console.Write("Joueur 2, veuillez saisir votre symbole de jeu\n");
            do
            {
                input = Console.ReadLine();

            } while (string.IsNullOrEmpty(input));
            char playerSymbol_2 = input[0];

            playerList.Add(new Player { PlayerName = playerName_1, PlayerSymbol = playerSymbol_1 });
            playerList.Add(new Player { PlayerName = playerName_2, PlayerSymbol = playerSymbol_2 });
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
        }
    }
}
