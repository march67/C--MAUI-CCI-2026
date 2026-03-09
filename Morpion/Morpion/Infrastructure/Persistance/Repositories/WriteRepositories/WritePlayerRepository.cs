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
    
    public void SaveAsync(Player player)
    {
        _dbContext.Players.Add(player);
        _dbContext.SaveChanges();
    }
    
    
}