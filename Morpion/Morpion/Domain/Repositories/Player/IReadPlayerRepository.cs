namespace Morpion.Domain.Repositories.Player;

public interface IReadPlayerRepository
{
    Task<Entities.Player> FindByNameAsync(string name);
}