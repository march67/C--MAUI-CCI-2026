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
        return await _dbContext.Games
            .Include(g => g.Winner)
            .Include(g => g.Loser)
            .FirstOrDefaultAsync(g => g.IsCompleted == false);
    }

    public async Task<int> NumberOfGamePlayedByPlayer(Player player)
    {
        return await _dbContext.Games.CountAsync(g => g.Winner == player || g.Loser == player);
    }

    public async Task<int> NumberOfGameWonByPlayer(Player player)
    {
        return await _dbContext.Games.CountAsync(g => g.Winner == player );
    }

    public async Task<decimal> PourcentageOfGameWonByPlayerAgainstBot(Player player)
    {
        int numberOfGamePlayedAgainstBot = await _dbContext.Games
            .Include(g => g.Winner)
            .Include(g => g.Loser)
            .CountAsync(g =>
                (g.Winner == player && g.Loser.IsHuman == false) ||
                (g.Loser == player && g.Winner.IsHuman == false)) ;
        
        if (numberOfGamePlayedAgainstBot == 0) return 0;
        
        int numberOfGameWonAgainstBot  = await _dbContext.Games
            .Include(g => g.Winner)
            .Include(g => g.Loser)
            .CountAsync(g => g.Winner == player && g.Loser.IsHuman == false);
        
        return (decimal)numberOfGameWonAgainstBot / numberOfGamePlayedAgainstBot * 100;
    }
}