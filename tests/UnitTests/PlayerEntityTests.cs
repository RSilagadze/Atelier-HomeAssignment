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
        public void ShouldCalculateBMIForNovakDjokovic()
        {
            //given
            const double targetBMI = 22.64;
            var player = new Player(52,"Novak","Djokovic","N.DJO","M",
                new Country("SRB","SRB"),
                "pic",
                new PlayerData(2, 2542, 80000, 188, 31, []));
            //when
            var result = player.CalculateBodyMassIndex();

            //then
            var delta = Math.Abs(targetBMI - result);
            Assert.True(delta <= 0.1);
        }
    }
}