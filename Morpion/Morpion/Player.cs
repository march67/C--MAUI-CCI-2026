using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    public class Player
    {
        public char PlayerSymbol { get; set; }
        public string PlayerName { get; set; }

        public bool CheckPlayerName()
        {
            return true;
        }

        public bool CheckPlayerSymbol()
        {
            return true;
        }
    }
}
