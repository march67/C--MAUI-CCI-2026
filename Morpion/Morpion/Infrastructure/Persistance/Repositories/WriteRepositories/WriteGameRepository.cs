using Morpion.Domain.Entities;

namespace Morpion.Infrastructure.Persistance.Repositories;

public class WriteGameRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public WriteGameRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async void SaveAsync(Game game)
    {
        _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();
    }
}