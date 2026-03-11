using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class RandomBotService : IBotService
    {
        private readonly Random _random = new();

        public int ChooseCell(Board board, string botSymbol)
        {
            var emptyCells = board.GetEmptyCells();

            if (emptyCells.Count == 0) return -1;

            int randomIndex = _random.Next(emptyCells.Count);
            return emptyCells[randomIndex];
        }
    }
}
