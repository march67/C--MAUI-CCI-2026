using MauiApp1Test.IFakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1Test.Fakes
{
    public class FakeGameHistory : IGameHistory
    {
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int Draws { get; private set; }

        public void RecordWin()
        {
            Wins++;
        }
        public void RecordLoss()
        {
            Losses++;
        }
        public void RecordDraw()
        {
            Draws++;
        }
    }
}
