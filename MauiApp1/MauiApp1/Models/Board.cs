using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Board
    {
        private readonly string[] _cells = new string[9];

        public string[] Cells => _cells;

        public string GetCell(int index)
        {
           return _cells[index];
        }
        public bool SetCell(int index, string symbol)
        {
            if (index < 0 || index > 8) return false; // out of range
            if (!string.IsNullOrEmpty(_cells[index])) return false;

            _cells[index] = symbol;
            return true;
        }

        public bool IsCellEmpty(int index) => string.IsNullOrEmpty(_cells[index]);

        public bool CheckWin(string symbol) =>
            (_cells[0] == symbol && _cells[1] == symbol && _cells[2] == symbol) ||
            (_cells[3] == symbol && _cells[4] == symbol && _cells[5] == symbol) ||
            (_cells[6] == symbol && _cells[7] == symbol && _cells[8] == symbol) ||
            (_cells[0] == symbol && _cells[3] == symbol && _cells[6] == symbol) ||
            (_cells[1] == symbol && _cells[4] == symbol && _cells[7] == symbol) ||
            (_cells[2] == symbol && _cells[5] == symbol && _cells[8] == symbol) ||
            (_cells[0] == symbol && _cells[4] == symbol && _cells[8] == symbol) ||
            (_cells[2] == symbol && _cells[4] == symbol && _cells[6] == symbol);

        public bool CheckDraw()
        {
            foreach (var c in _cells)
                if (string.IsNullOrEmpty(c)) return false;
            return true;
        }

        public void Reset()
        {
            for (int i = 0; i < 9; i++)
                _cells[i] = string.Empty;
        }

        public List<int> GetEmptyCells()
        {
            // Cell disponible pour l'input du bot
            var empty = new List<int>();
            for (int i = 0; i < 9; i++)
                if (string.IsNullOrEmpty(_cells[i])) empty.Add(i);
            return empty;
        }
    }
}
