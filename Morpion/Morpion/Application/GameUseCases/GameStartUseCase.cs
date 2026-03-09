using Morpion.Domain.Entities;
using Morpion.Domain.Repositories.Game;

namespace Morpion.Application.GameUseCases;

public class GameStartUseCase
{
    private readonly IReadGameRepository _readGameRepository;
    private readonly IWriteGameRepository _writeGameRepository;

    public GameStartUseCase(IReadGameRepository readGameRepository, IWriteGameRepository writeGameRepository)
    {
        _readGameRepository = readGameRepository;
        _writeGameRepository = writeGameRepository;
    }

    public async Task<Game> CreateGame(Game game)
    {
        Game ongoingGame = await _readGameRepository.FindOngoingGame();
        if (ongoingGame == null)
        {
            await _writeGameRepository.SaveAsync(game);
            return game;
        }
        
        return ongoingGame;
    }

    public async Task UpdateGame(Game game)
    {
        await _writeGameRepository.UpdateAsync(game);
    }

    public async Task ShowPlayersResult(Player player1, Player player2 )
    {
        int winCountPlayer1 = await _readGameRepository.NumberOfGamePlayedByPlayer(player1);
        int winCountPlayer2 = await _readGameRepository.NumberOfGamePlayedByPlayer(player2);
        
        Console.WriteLine($"Nombre de partie jouées par " + player1.Name + " : " + winCountPlayer1);
        Console.WriteLine($"Nombre de partie jouées par " + player2.Name + " : " + winCountPlayer2);
    }

    public void RetrieveOngoingGameState(Game game)
    {
        // to do
    }
}