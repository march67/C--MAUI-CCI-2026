using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGameHistory _gameHistory;
        private readonly IBotService _botService;
        private readonly Board _board;
        private bool _gameEnded = false;

        // Le joueur humain joue toujours X, le bot joue O
        private const string HumanSymbol = "X";
        private const string BotSymbol = "O";

        public string Cell0 => _board.GetCell(0);
        public string Cell1 => _board.GetCell(1);
        public string Cell2 => _board.GetCell(2);
        public string Cell3 => _board.GetCell(3);
        public string Cell4 => _board.GetCell(4);
        public string Cell5 => _board.GetCell(5);
        public string Cell6 => _board.GetCell(6);
        public string Cell7 => _board.GetCell(7);
        public string Cell8 => _board.GetCell(8);

        private void NotifyCell(int index) =>
         OnPropertyChanged($"Cell{index}");


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string _statusText = "Votre tour (X)";
        public string StatusText
        {
            get => _statusText;
            private set { _statusText = value; OnPropertyChanged(); }
        }

        public string RecordText
        {
            get { return _gameHistory.DisplayHistory(); }
        }

        public ICommand CellClickCommand { get; }
        public ICommand ReplayCommand { get; }

        public MainViewModel(IBotService botService, IGameHistory gameHistory)
        {
            _gameHistory = gameHistory;
            _botService = botService;
            _board = new Board();
            CellClickCommand = new Command<string>(ExecuteCellClick);
            ReplayCommand = new Command(ExecuteReplay);
        }


        private void ExecuteCellClick(string indexStr)
        {
            if (_gameEnded) return;
            if (!int.TryParse(indexStr, out int index)) return;
            if (!_board.IsCellEmpty(index)) return;

            // Human inp
            _board.SetCell(index, HumanSymbol);
            NotifyCell(index);

            if (CheckEndGame(HumanSymbol)) return;

            // Bot inp
            StatusText = "Tour du bot...";
            int botIndex = _botService.ChooseCell(_board, BotSymbol);

            if (botIndex == -1) return;

            _board.SetCell(botIndex, BotSymbol);
            NotifyCell(botIndex);

            CheckEndGame(BotSymbol);
        }

        private bool CheckEndGame(string symbol)
        {
            if (_board.CheckWin(symbol))
            {
                StatusText = symbol;
                if (StatusText == HumanSymbol)
                {
                    Console.WriteLine("Vous avez gagné !");
                    _gameHistory.RecordWin();
                    OnPropertyChanged(nameof(RecordText));
                }
                else
                {
                    Console.WriteLine("Le bot a gagné !");
                    _gameHistory.RecordLoss();
                    OnPropertyChanged(nameof(RecordText));
                }
                
                _gameEnded = true;
                
                return true;
            }

            if (_board.CheckDraw())
            {
                StatusText = "Match nul !";
                _gameEnded = true;
                _gameHistory.RecordDraw();
                OnPropertyChanged(nameof(RecordText));
                return true;
            }

            if (symbol == BotSymbol)
                StatusText = "Votre tour X";

            return false;
        }

        private void ExecuteReplay()
        {
            _board.Reset();

            for (int i = 0; i < 9; i++)
                NotifyCell(i);

            //_playerTurnX = true;
            _gameEnded = false;
            StatusText = "Votre tour X";
        }


    }
}
