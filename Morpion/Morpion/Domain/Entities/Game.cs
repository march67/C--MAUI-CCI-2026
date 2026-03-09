namespace Morpion.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public Guid WinnerId { get; set; }
    public Guid LoserId  { get; set; } 
    public Player Winner { get; set; }
    public Player Loser  { get; set; }
    public bool isCompleted { get; set; }

    private Game() {}
}