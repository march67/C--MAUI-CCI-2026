namespace Morpion.Domain.Repositories.Game;

public interface IReadGameRepository
{
    Task<Entities.Game> FindOngoingGame();
}