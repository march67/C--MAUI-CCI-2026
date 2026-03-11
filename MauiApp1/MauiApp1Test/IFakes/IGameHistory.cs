using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1Test.IFakes
{
    public interface IGameHistory
    {
        void RecordWin();
        void RecordLoss();
        void RecordDraw();
        int Wins { get; }
        int Losses { get; }
        int Draws { get; }
    }
}
