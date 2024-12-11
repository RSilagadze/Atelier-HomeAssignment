using Domain.Entities;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldGetBMIForNovakDjokovic()
        {
            //arrange
            const double targetBMI = 22.64;
            var player = new Player(52,"Novak","Djokovic","N.DJO","M",
                new Country("SRB","SRB"),
                "pic",
                new PlayerData(2, 2542, 80000, 188, 31, []));
            //act
            var result = player.GetBodyMassIndex();

            //assert
            Assert.That(targetBMI - result, Is.LessThanOrEqualTo(0.1));
        }

        [Test]
        public void ShouldThrowInvalidOperationExceptionWhenHeightAndWeightAreBelowOrEqual0()
        {
            //arrange
            var playerNegativeHeight = new Player(52, "Novak", "Djokovic", "N.DJO", "M",
                new Country("SRB", "SRB"),
                "pic",
                new PlayerData(2, 2542, 80000, -1, 31, []));
            var playerNegativeWeight = new Player(52, "Novak", "Djokovic", "N.DJO", "M",
                new Country("SRB", "SRB"),
                "pic",
                new PlayerData(2, 2542, -80000, 188, 31, []));

            //act & act
            Assert.Throws<InvalidOperationException>(() =>
            {
                playerNegativeHeight.GetBodyMassIndex();
            });
            Assert.Throws<InvalidOperationException>(() =>
            {
                playerNegativeWeight.GetBodyMassIndex();
            });

        }
    }
}