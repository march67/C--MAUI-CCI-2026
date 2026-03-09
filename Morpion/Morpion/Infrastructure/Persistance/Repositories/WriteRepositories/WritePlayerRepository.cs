using Morpion.Domain.Entities;
using Morpion.Domain.Repositories.Player;

namespace Morpion.Infrastructure.Persistance.Repositories;

public class WritePlayerRepository : IWritePlayerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public WritePlayerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Player> SaveAsync(Player player)
    {
        await _dbContext.Players.AddAsync(player);
        await _dbContext.SaveChangesAsync();
        return  player;
    }
    
    
}