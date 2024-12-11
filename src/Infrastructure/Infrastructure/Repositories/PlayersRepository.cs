using System.Text.Json;
using Domain.Entities;
using Domain.Ports;
using Infrastructure.DTOs;

namespace Infrastructure.Repositories
{
    public sealed class PlayersRepository(string jsonDataFilePath, JsonSerializerOptions? jsonSerializerOptions = null)
        : IPlayersRepository
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions ?? JsonSerializerOptions.Default;

        private static Player PlayerDTOToPlayerEntity(PlayerDbDTO playerDbDto)
        {
            var country = new Country(playerDbDto.Country.Picture, playerDbDto.Country.Code);
          
            var data = new PlayerData(playerDbDto.Data.Rank,
                playerDbDto.Data.Points,
                playerDbDto.Data.Weight,
                playerDbDto.Data.Height,
                playerDbDto.Data.Age,
                playerDbDto.Data.Last);
          
            return new Player(playerDbDto.Id,
                playerDbDto.FirstName,
                playerDbDto.LastName,
                playerDbDto.ShortName,
                playerDbDto.Sex,
                country,
                playerDbDto.Picture,
                data);
        }

        public async  Task<IEnumerable<Player>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = new List<Player>(32);
            await using var fs = File.OpenRead(jsonDataFilePath);
            var root = await JsonSerializer.DeserializeAsync<RootDbDTO>(fs, _jsonSerializerOptions, cancellationToken);
            
            if (root == null || !root.Players.Any())
                return [];

            entities.AddRange(root.Players.Select(PlayerDTOToPlayerEntity));

            return entities;
        }

        public async Task<Player?> GetAsync(Func<Player, bool> predicationFunc, CancellationToken cancellationToken = default)
        {
            Player? foundPlayer = null;
            await using var fs = File.OpenRead(jsonDataFilePath);
            var root = await JsonSerializer.DeserializeAsync<RootDbDTO>(fs, _jsonSerializerOptions, cancellationToken);
            if (root == null || !root.Players.Any())
                return null;

            foreach (var playerDTO in root.Players)
            {
                var playerEntity = PlayerDTOToPlayerEntity(playerDTO);
                if (predicationFunc(playerEntity))
                    return playerEntity;
            }

            return foundPlayer;
        }
    }
}
