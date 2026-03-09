namespace Morpion.Domain.Repositories.Player;

public interface IWritePlayerRepository
{
    void SaveAsync(Entities.Player player);
}