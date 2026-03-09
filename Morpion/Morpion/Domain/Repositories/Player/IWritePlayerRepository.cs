namespace Morpion.Domain.Repositories.Player;

public interface IWritePlayerRepository
{
    Task<Entities.Player> SaveAsync(Entities.Player player);
}