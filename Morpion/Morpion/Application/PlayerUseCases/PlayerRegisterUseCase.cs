using Morpion.Domain.Entities;
using Morpion.Domain.Repositories.Player;

namespace Morpion.Application.PlayerUseCases;

public class PlayerRegisterUseCase
{
    private readonly IWritePlayerRepository _writePlayerRepository;
    private readonly IReadPlayerRepository _readPlayerRepository;

    public PlayerRegisterUseCase(IWritePlayerRepository writePlayerRepository, IReadPlayerRepository readPlayerRepository)
    {
        _writePlayerRepository = writePlayerRepository;
        _readPlayerRepository = readPlayerRepository;
    }

    public async Task<Player> Save(Player playerToSave)
    {
        Player player = await _readPlayerRepository.FindByNameAsync(playerToSave.Name);
        if (player == null)
        {
            await _writePlayerRepository.SaveAsync(playerToSave);
            return playerToSave;
        }

        return player;
    }
}