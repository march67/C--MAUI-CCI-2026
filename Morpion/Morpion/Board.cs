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

        public char[,] board;

        public Board()
        {
            board = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        public void InputMoveOnBoard((int row, int column, char symbol) positionWithSymbol)
        {
            board[positionWithSymbol.row - 1, positionWithSymbol.column - 1] = positionWithSymbol.symbol;
        }

        public void DisplayBoard()
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

        public bool CheckWinCondition(string playerName)
        {
            if (CheckRowWinCondition() || CheckColumnWinCondition() || CheckDiagonalWinCondition())
            {
                Console.Write($"\nLe joueur : " + playerName + " a gagné");
                return true;
            }

            return false;
        }

        public virtual bool CheckDiagonalWinCondition()
        {
            if ((board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] != ' ')
                || (board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0] && board[0, 2] != ' '))
            {
                return true;
            }

            return false;
        }

        public virtual bool CheckColumnWinCondition()
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

        public virtual bool CheckRowWinCondition()
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

        public bool CheckEndGame()
        {
            bool boardIsFull = board.Cast<char>().All(c => c != ' '); // return un IEnumerable<char> pour pouvoir utiliser .All
            if (boardIsFull)
            {

                Console.Write($"\nFin de partie, aucun gagnant");
            }

            return boardIsFull;

        }

        public bool CheckValidCellForInput(int row, int column, IPlayer player)
        {
            if (board[row, column] != ' ')
            {
                if (player is HumanPlayer)
                {
                    Console.WriteLine("\nVeuillez choisir une cellule vide");
                }
                return false;
            }

            return true;
        }
    }
}
