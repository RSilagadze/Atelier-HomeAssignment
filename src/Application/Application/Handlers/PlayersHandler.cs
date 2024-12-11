using Application.Ports;
using Domain.Entities;
using Domain.Ports;
using Domain.Exceptions;

namespace Application.Handlers
{
    public sealed class PlayersHandler(IPlayersRepository playersRepository) : IPlayersHandler
    {
        public async Task<IEnumerable<Player>> GetAllPlayersAsync(CancellationToken cancellationToken = default)
        {
            var players = await playersRepository.GetAllAsync(cancellationToken);
            return players.OrderByDescending(x => x.Data.Points);
        }

        public async Task<Player> GetPlayerByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var player = await playersRepository.GetAsync(p => p.Id == id, cancellationToken);
            if (player == null)
                throw new PlayerNotFoundException(id);
            return player;
        }

        public async Task<PlayersDirectory> GetPlayersDirectoryAsync(CancellationToken cancellationToken = default)
        {
           var players = await playersRepository.GetAllAsync(cancellationToken);

           return new PlayersDirectory(players);
        }
    }
}
