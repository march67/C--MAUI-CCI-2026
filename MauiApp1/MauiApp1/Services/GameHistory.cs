using MauiApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class GameHistory : IGameHistory
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public void RecordDraw()
        {
            Draws++;
        }

        public void RecordLoss()
        {
            Losses++;
        }

        public void RecordWin()
        {
            Wins++;
        }

        public string DisplayHistory()
        {
            return $"Nb de victoires : " + Wins + " Nb de défaites : " + Losses + " Nb de matchs nuls : " + Draws;
        }
    }
}
