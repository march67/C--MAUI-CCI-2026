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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private readonly string[] _board = new string[9];
        private bool _playerTurnX = true;
        private bool _gameEnded = false;

        public string Cell0 { get => _board[0]; private set { _board[0] = value; OnPropertyChanged(); } }
        public string Cell1 { get => _board[1]; private set { _board[1] = value; OnPropertyChanged(); } }
        public string Cell2 { get => _board[2]; private set { _board[2] = value; OnPropertyChanged(); } }
        public string Cell3 { get => _board[3]; private set { _board[3] = value; OnPropertyChanged(); } }
        public string Cell4 { get => _board[4]; private set { _board[4] = value; OnPropertyChanged(); } }
        public string Cell5 { get => _board[5]; private set { _board[5] = value; OnPropertyChanged(); } }
        public string Cell6 { get => _board[6]; private set { _board[6] = value; OnPropertyChanged(); } }
        public string Cell7 { get => _board[7]; private set { _board[7] = value; OnPropertyChanged(); } }
        public string Cell8 { get => _board[8]; private set { _board[8] = value; OnPropertyChanged(); } }

        private string _statusText = "Tour de X";
        public string StatusText
        {
            get => _statusText;
            private set { _statusText = value; OnPropertyChanged(); }
        }

        public ICommand CellClickCommand { get; }
        public ICommand ReplayCommand { get; }

        public MainViewModel()
        {
            CellClickCommand = new Command<string>(ExecuteCellClick);
            ReplayCommand = new Command(ExecuteReplay);
        }

        private void ExecuteCellClick(string indexStr)
        {
            if (_gameEnded) return;
            if (!int.TryParse(indexStr, out int index)) return;
            if (!string.IsNullOrEmpty(_board[index])) return;

            string symbole = _playerTurnX ? "X" : "O";

            SetCell(index, symbole);

            if (CheckWin(symbole))
            {
                StatusText = $"{symbole} a gagné !";
                _gameEnded = true;
            }
            else if (CheckDraw())
            {
                StatusText = "Match nul !";
                _gameEnded = true;
            }
            else
            {
                _playerTurnX = !_playerTurnX;
                StatusText = $"Tour de {(_playerTurnX ? "X" : "O")}";
            }
        }
        private void ExecuteReplay()
        {
            for (int i = 0; i < 9; i++)
                SetCell(i, string.Empty);

            _playerTurnX = true;
            _gameEnded = false;
            StatusText = "Tour de X";
        }

        private void SetCell(int index, string value)
        {
            _board[index] = value;
            OnPropertyChanged($"Cell{index}");
        }

        private bool CheckWin(string symbol) =>
            (_board[0] == symbol && _board[1] == symbol && _board[2] == symbol) ||
            (_board[3] == symbol && _board[4] == symbol && _board[5] == symbol) ||
            (_board[6] == symbol && _board[7] == symbol && _board[8] == symbol) ||
            (_board[0] == symbol && _board[3] == symbol && _board[6] == symbol) ||
            (_board[1] == symbol && _board[4] == symbol && _board[7] == symbol) ||
            (_board[2] == symbol && _board[5] == symbol && _board[8] == symbol) ||
            (_board[0] == symbol && _board[4] == symbol && _board[8] == symbol) ||
            (_board[2] == symbol && _board[4] == symbol && _board[6] == symbol);

        private bool CheckDraw()
        {
            foreach (var c in _board)
                if (string.IsNullOrEmpty(c)) return false;
            return true;
        }
    }
}
