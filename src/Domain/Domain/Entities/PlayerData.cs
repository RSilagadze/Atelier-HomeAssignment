namespace Domain.Entities
{
    public sealed class PlayerData(int rank, int points, double weight, double height, int age, IEnumerable<int> last)
    {
        public int Rank { get; } = rank;
        public int Points { get; } = points;
        public double Weight { get; } = weight;
        public double Height { get; } = height;
        public int Age { get; } = age;
        public IEnumerable<int> Last { get; set; } = last;
    }
}
