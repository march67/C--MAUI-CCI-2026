using Microsoft.EntityFrameworkCore;
using Morpion.Domain.Entities;
using Morpion.Domain.Repositories.Player;

namespace Morpion.Infrastructure.Persistance.Repositories.ReadRepositories;

public class ReadPlayerRepository : IReadPlayerRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ReadPlayerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Player> FindByNameAsync(string name)
    {
        return await _dbContext.Players.FirstOrDefaultAsync(p => p.Name == name);
    }
}