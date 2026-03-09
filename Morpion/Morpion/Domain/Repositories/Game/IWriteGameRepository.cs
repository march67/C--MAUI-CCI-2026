namespace Morpion.Domain.Repositories.Game;

public interface IWriteGameRepository
{
    Task SaveAsync(Entities.Game game);
    
    Task UpdateAsync(Entities.Game game);
}