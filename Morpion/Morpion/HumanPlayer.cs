using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    public class HumanPlayer : IPlayer
    {
        public char HumanSymbol;

        public string HumanName;


        public bool CheckPlayerName()
        {
            return true;
        }

        public bool CheckPlayerSymbol()
        {
            return true;
        }

        public string GetPlayerName()
        {
            return this.HumanName;
        }

        public char GetPlayerSymbol()
        {
            return this.HumanSymbol;
        }

        public async Task<(int, int, char)> PlayerInput(Board board)
        {
            await Task.CompletedTask;

            int rowInput;
            int columnInput;

            Console.WriteLine($"\nTour du joueur " + GetPlayerName());

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
            } while (!board.CheckValidCellForInput(rowInput - 1, columnInput - 1, this));

            Console.WriteLine("\nVeuillez choisir une cellule vide");

            Console.Write("\n");
            
            return (rowInput, columnInput, GetPlayerSymbol());
        }
    }
}
