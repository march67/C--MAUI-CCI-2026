using Microsoft.EntityFrameworkCore;
using Morpion.Domain.Entities;
using Morpion.Domain.Repositories.Game;

namespace Morpion.Infrastructure.Persistance.Repositories.ReadRepositories;

public class ReadGameRepository : IReadGameRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ReadGameRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Game> FindOngoingGame()
    {
        return await _dbContext.Games.FirstOrDefaultAsync(g => g.IsCompleted == false);
    }
}