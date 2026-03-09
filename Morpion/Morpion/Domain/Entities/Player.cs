namespace Morpion.Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsHuman  { get; set; }
    
    private Player() { }

    public static Player Create(string name, bool isHuman)
    {
        return new Player
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsHuman = isHuman
        };
    }
}