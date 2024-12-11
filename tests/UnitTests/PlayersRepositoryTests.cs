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
            //arrange
            const int recordsCount = 5;

            //act
            var players = await _playersRepository.GetAllAsync();

            //assert
            Assert.That(players.Count(), Is.EqualTo(recordsCount));
        }

        [Test]
        public async Task ShouldReadPlayerWithId52FromLocalJsonFileAsync()
        {
            //arrange
            const int id = 52;

            //act
            var player = await _playersRepository.GetAsync(p => p.Id == id);

            //assert
            Assert.That(player is { Id: id });
        }
    }
}