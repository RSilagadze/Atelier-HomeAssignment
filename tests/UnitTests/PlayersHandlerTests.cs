using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Handlers;
using Domain.Entities;
using Domain.Ports;
using Infrastructure.Repositories;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace UnitTests
{
    public class PlayersHandlerTests
    {
        private IPlayersRepository _playersRepository;
        private List<Player> _players;

        [SetUp]
        public void Setup()
        {
            _players = [];
            var djo = new Player(1, "Novak", "Djokovic", "N.DJO", "M",
                new Country("SRB", "SRB"),
                "pic",
                new PlayerData(99, 2542, 80000, 188, 31, []));

            var swil = new Player(2, "Serena", "Williams", "S.WIL", "F",
                new Country("USA", "USA"),
                "pic",
                new PlayerData(50, 3521, 72000, 175, 37, []));

            var vwil = new Player(3, "Venus", "Williams", "V.VIL", "F",
                new Country("USA", "USA"),
                "pic",
                new PlayerData(1, 1105, 74000, 185, 38, []));

            _players.Add(djo);
            _players.Add(swil);
            _players.Add(vwil);
        

            _playersRepository = Substitute.For<IPlayersRepository>();
            _playersRepository.GetAllAsync().Returns(Task.FromResult(_players.AsEnumerable()));
            _playersRepository.GetAsync(Arg.Any<Func<Player, bool>>())
                .Returns(info =>
                {
                    var predicate = info.ArgAt<Func<Player, bool>>(0);
                    return Task.FromResult(_players.FirstOrDefault(predicate));
                });
        }

        [Test]
        public async Task ShouldCallRepositoryFromHandlerAsync()
        {
            //arrange
            var handler = new PlayersHandler(_playersRepository);

            //act
            await handler.GetPlayerByIdAsync(1);
            await handler.GetAllPlayersAsync();
            await handler.GetPlayersDirectoryAsync();

            //assert
            await _playersRepository
                .Received(Quantity.Exactly(1))
                .GetAsync(Arg.Any<Func<Player, bool>>());
            await _playersRepository
                .Received(Quantity.AtLeastOne())
                .GetAllAsync();
        }

        [Test]
        public async Task ShouldOrderPlayersByRankAscAsync()
        {
            //arrange
            var handler = new PlayersHandler(_playersRepository);
            const string expectedNameAtRank1 = "Venus";

            //act
            var players = await handler.GetAllPlayersAsync();

            //assert
            Assert.That(players.First().Firstname, Is.EqualTo(expectedNameAtRank1));
        }
    }
}