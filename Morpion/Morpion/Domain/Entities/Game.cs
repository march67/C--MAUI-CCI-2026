namespace Morpion.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public Guid WinnerId { get; set; }
    public Guid LoserId  { get; set; } 
    public Player Winner { get; set; }
    public Player Loser  { get; set; }
    public char? Player1Symbol  { get; set; }
    public char? Player2Symbol  { get; set; }
    public bool IsCompleted { get; set; }
    public string? BoardState { get; set; }

    private Game() {}

    public static Game Create(Player player1, Player player2, char symbol1, char symbol2)
    {
        return new Game()
        {
            Id = Guid.NewGuid(),
            Winner = player1,
            WinnerId = player1.Id,
            Player1Symbol = symbol1,
            Loser = player2,
            LoserId =  player2.Id,
            Player2Symbol = symbol2,
            IsCompleted = false,
        };
    }

    public void SetResult(Player winner, Player loser)
    {
        Winner = winner;
        WinnerId = winner.Id;
        Loser = loser;
        LoserId = loser.Id;
        IsCompleted = true;
    }

    public void SaveBoardState(char[,] board)
    {
        BoardState = string.Join("", board.Cast<char>());
    }

    public char[,] LoadBoardState()
    {
        if (string.IsNullOrEmpty(BoardState)) return new char[3, 3];
        
        char[,] board = new char[3, 3];
        int index = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = BoardState[index++];
        
        return board;
    }
}