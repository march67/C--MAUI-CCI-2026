using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    public interface IPlayer
    {
        bool CheckPlayerName();

        bool CheckPlayerSymbol();

        string GetPlayerName();

        char GetPlayerSymbol();

        Task<(int, int, char)> PlayerInput(Board board);
    }
}
