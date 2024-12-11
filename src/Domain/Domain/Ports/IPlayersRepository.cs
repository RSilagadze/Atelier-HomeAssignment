using Domain.Entities;

namespace Domain.Ports
{
    public interface IPlayersRepository
    {
        Task <IEnumerable<Player>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Player?> GetAsync(Func<Player, bool> predicationFunc,CancellationToken cancellationToken = default);
    }
}
