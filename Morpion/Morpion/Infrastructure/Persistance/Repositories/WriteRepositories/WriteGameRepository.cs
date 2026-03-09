using Morpion.Domain.Entities;
using Morpion.Domain.Repositories.Game;

namespace Morpion.Infrastructure.Persistance.Repositories;

public class WriteGameRepository : IWriteGameRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public WriteGameRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(Game game)
    {
        await _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game) 
    {
        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync();
    }
}