using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Ports;
using Infrastructure.Repositories;

namespace UnitTests
{
    public class PlayersRepositoryTests
    {
        private IPlayersRepository _playersRepository;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
        };

        [SetUp]
        public void Setup()
        {
            _playersRepository = new PlayersRepository("headtohead.json", _jsonSerializerOptions);
        }

        [Test]
        public async Task ShouldReadAll5RecordsDataFromLocalJsonFileAsync()
        {
            //given
            const int recordsCount = 5;

            //when
            var players = await _playersRepository.GetAllAsync();

            //then
            Assert.That(players.Count() == recordsCount);
        }

        [Test]
        public async Task ShouldReadPlayerWithId52FromLocalJsonFileAsync()
        {
            //given
            const int id = 52;

            //when
            var player = await _playersRepository.GetAsync(p => p.Id == id);

            //then
            Assert.That(player is { Id: id });
        }
    }
}