namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private bool PlayerTurnX = true;
        private bool GameEnded = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCellClick(object sender, EventArgs e)
        {
            Button bouton = (Button)sender;

            if (!string.IsNullOrEmpty(bouton.Text) || GameEnded)
                return;

            bouton.Text = PlayerTurnX ? "X" : "O";

            if (CheckWin())
            {
                LabelStatus.Text = $"{bouton.Text} a gagné !";
                GameEnded = true;
                return;
            }

            if (CheckDraw())
            {
                LabelStatus.Text = "Match nul !";
                GameEnded = true;
                return;
            }

            PlayerTurnX = !PlayerTurnX;
            LabelStatus.Text = PlayerTurnX ? "Tour de X" : "Tour de O";
        }

        private bool CheckWin()
        {
            Button[] cases = new Button[9];
            int index = 0;
            foreach (var child in GrilleMorpion.Children)
            {
                if (child is Button b)
                    cases[index++] = b;
            }

            // Les 8 combinaisons gagnantes
            int[][] combinaisons = new int[][]
            {
            new[] {0, 1, 2}, // ligne 0
            new[] {3, 4, 5}, // ligne 1
            new[] {6, 7, 8}, // ligne 2
            new[] {0, 3, 6}, // colonne 0
            new[] {1, 4, 7}, // colonne 1
            new[] {2, 5, 8}, // colonne 2
            new[] {0, 4, 8}, // diagonale
            new[] {2, 4, 6}, // anti-diagonale
            };

            foreach (var combo in combinaisons)
            {
                string a = cases[combo[0]].Text;
                string b = cases[combo[1]].Text;
                string c = cases[combo[2]].Text;

                if (!string.IsNullOrEmpty(a) && a == b && b == c)
                    return true;
            }

            return false;
        }

        private bool CheckDraw()
        {
            foreach (var enfant in GrilleMorpion.Children)
            {
                if (enfant is Button b && string.IsNullOrEmpty(b.Text))
                    return false;
            }
            return true;
        }

        private void OnReplayClick(object sender, EventArgs e)
        {
            foreach (var child in GrilleMorpion.Children)
            {
                if (child is Button b)
                {
                    b.Text = "";
                }
            }

            PlayerTurnX = true;
            GameEnded = false;
            LabelStatus.Text = "Tour de X";
        }

        private void ChangePlayerTurn()
        {

        }

        private void RandomizeFirstTurn()
        {

        }

        private void InputPlayerName()
        {
           
        }
    }
}
