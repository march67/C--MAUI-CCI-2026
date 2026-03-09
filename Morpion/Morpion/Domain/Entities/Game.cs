namespace Morpion.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public Guid WinnerId { get; set; }
    public Guid LoserId  { get; set; } 
    public Player Winner { get; set; }
    public Player Loser  { get; set; }
    public bool IsCompleted { get; set; }

    private Game() {}

    public static Game Create(Player player1, Player player2)
    {
        return new Game()
        {
            Id = Guid.NewGuid(),
            Winner = player1,
            Loser = player2,
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
}