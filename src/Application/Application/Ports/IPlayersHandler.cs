using Domain.Entities;

namespace Application.Ports
{
    public interface IPlayersHandler
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync(CancellationToken cancellationToken = default);
        Task<Player> GetPlayerByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<PlayersDirectory> GetPlayersDirectoryAsync(CancellationToken cancellationToken = default);
    }
}
