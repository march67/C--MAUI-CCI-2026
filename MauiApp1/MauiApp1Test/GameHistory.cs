using MauiApp1.Services;
using MauiApp1.ViewModels;
using MauiApp1Test.Fakes;

namespace MauiApp1Test
{
    public class GameHistory
    {
        [Fact]
        public void Win_Increments_Counter()
        {
            // prep
            var history = new FakeGameHistory();
            var iBotService = new RandomBotService();
            var vm = new MainViewModel(iBotService, history);

            // ligne win
            vm.CellClickCommand.Execute("0");
            vm.CellClickCommand.Execute("1");
            vm.CellClickCommand.Execute("2");

            // asserts
            Assert.Equal(1, history.Wins);
        }
    }
}
