using MauiApp1.Services;

namespace MauiApp1Test
{
    public class GameHistory
    {
        [Fact]
        public void Win_Increments_Counter()
        {
            var history = new FakeGameHistory();
            var vm = new MainViewModel(botService, fakeHistory);

            // Human win
            vm.CellClickCommand.Execute("0");
            vm.CellClickCommand.Execute("1");
            vm.CellClickCommand.Execute("2");

            Assert.Equal(1, fakeHistory.Wins);
        }
    }
}
