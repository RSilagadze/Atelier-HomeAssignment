using Domain.Entities;

namespace UnitTests
{
    public class PlayersDirectoryTests
    {
        private List<Player> _players;

        [SetUp]
        public void Setup()
        {
            _players = [];
            var djo = new Player(1, "Novak", "Djokovic", "N.DJO", "M",
                new Country("SRB", "SRB"),
                "pic",
                new PlayerData(2, 2542, 80000, 188, 31, []));
           
            var swil = new Player(2, "Serena", "Williams", "S.WIL", "F",
                new Country("USA", "USA"),
                "pic",
                new PlayerData(10, 3521, 72000, 175, 37, []));
           
            var vwil = new Player(3, "Venus", "Williams", "V.VIL", "F",
                new Country("USA", "USA"),
                "pic",
                new PlayerData(52, 1105, 74000, 185, 38, []));

            _players.Add(djo);
            _players.Add(swil);
            _players.Add(vwil);
        }

        [Test]
        public void ShouldTrowInvalidOperationExceptionOnNullOrEmptyPlayersList()
        {
            //arrange
            var playersEmptyList = new List<Player>();
            List<Player> playersNull = null;

            //act & act
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = new PlayersDirectory(playersEmptyList);
            });
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = new PlayersDirectory(playersNull);
            });
        }

        [Test]
        public void ShouldFindTheCountryWithHighestPlayersScore()
        {
            //arrange
            var directory = new PlayersDirectory(_players);
            const string targetCountry = "USA";
            
            //act
            var country = directory.GetCountryWithHighestWinningPoints();

            //assert
            Assert.That(string.Equals(targetCountry, country.Code, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void ShouldGetTheAveragePlayersBMI()
        {
            //arrange
            var directory = new PlayersDirectory(_players);
            const double targetAvgBMIApprox = 22.59d;

            //act
            var avgBMI = directory.GetAverageBodyMassIndex();

            //assert
            Assert.That(avgBMI, Is.EqualTo(targetAvgBMIApprox));
        }

        [Test]
        public void ShouldGetTheMediumPlayersHeight()
        {
            //arrange
            var directory = new PlayersDirectory(_players);
            const double targetMedianHeight = 185;

            //act
            var medianHeight = directory.GetMedianHeightForAllPlayers();

            //assert
            Assert.That(medianHeight, Is.EqualTo(targetMedianHeight));
        }
    }
}