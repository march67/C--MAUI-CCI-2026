using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    public class Board
    {
        const string VerticalSeparator = "|";
        const string HorizontalSeparator = "------";

        private Player CurrentPlayerToPlay;
        private bool IsFirstTurnOfTheGame = true;
        private readonly List<Player> playerList;

        private char[,] board;

        public Board(List<Player> playerList)
        {
            this.playerList = playerList;

            board = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }

            RandomizePlayerTurn(playerList);

            DisplayBoard();

            while(!CheckWinCondition() && !CheckEndGame())
            {
                ChangePlayerTurn();
                Input();
                DisplayBoard();
            }

            Console.Write($"\nLe joueur : " + CurrentPlayerToPlay.PlayerName + " a gagné");
        }

        private void DisplayBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(board[i, 0] + VerticalSeparator + board[i, 1] + VerticalSeparator + board[i, 2]);

                if (i != 2)
                {
                    Console.WriteLine(HorizontalSeparator);
                }
            }
        }

        private void Input()
        {
            int rowInput;
            int columnInput;
            char symbol = CurrentPlayerToPlay.PlayerSymbol;

            Console.WriteLine($"\nTour du joueur " + CurrentPlayerToPlay.PlayerName);


            do
            {
                Console.Write("Entrez la ligne : ");
                while (!int.TryParse(Console.ReadLine(), out rowInput))
                {
                    Console.Write("Saisie invalide. Entrez la ligne : ");
                }

                Console.Write("Entrez la colonne : ");
                while (!int.TryParse(Console.ReadLine(), out columnInput))
                {
                    Console.Write("Saisie invalide. Entrez la colonne : ");
                }
            } while (!CheckValidCellForInput(rowInput - 1, columnInput - 1));

            Console.Write("\n");

            board[rowInput - 1, columnInput - 1] = symbol;
        }

        private void ChangePlayerTurn()
        {
            if (!IsFirstTurnOfTheGame)
            {
                CurrentPlayerToPlay = playerList[0] == CurrentPlayerToPlay
                    ? playerList[1]
                    : playerList[0];
            }
            else
            {
                IsFirstTurnOfTheGame = false;
            }
        }

        private bool CheckWinCondition()
        {
            return CheckRowWinCondition() || CheckColumnWinCondition() || CheckDiagonalWinCondition();
        }

        private bool CheckDiagonalWinCondition()
        {
            if ((board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] != ' ')
                || (board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0] && board[0, 2] != ' '))
            {
                return true;
            }

            return false;
        }

        private bool CheckColumnWinCondition()
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == board[1, j] && board[1, j] == board[2, j] && board[0, j] != ' ')
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckRowWinCondition()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2] && board[i, 0] != ' ')
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckEndGame()
        {
            bool boardIsFull = board.Cast<char>().All(c => c != ' '); // return un IEnumerable<char> pour pouvoir utiliser .All
            if (boardIsFull)
            {

                Console.Write($"\nFin de partie, aucun gagnant");
            }

            return boardIsFull;

        }

        private void RandomizePlayerTurn(List<Player> playerList)
        {
            Random random = new Random();
            int index = random.Next(playerList.Count);
            CurrentPlayerToPlay = playerList[index];
        }

        private bool CheckValidCellForInput(int row, int column)
        {
            Console.WriteLine("\nVeuillez choisir une cellule vide");
            return board[row, column] == ' ' ? true : false;
        }
    }
}
