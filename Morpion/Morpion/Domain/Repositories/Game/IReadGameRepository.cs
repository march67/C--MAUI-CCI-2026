namespace Morpion.Domain.Repositories.Game;

public interface IReadGameRepository
{
    Task<Entities.Game> FindOngoingGame();
    
    Task<int> NumberOfGamePlayedByPlayer(Entities.Player player);
    
    Task<int> NumberOfGameWonByPlayer(Entities.Player player);
}