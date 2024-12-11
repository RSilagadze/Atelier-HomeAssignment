namespace Infrastructure.DTOs
{
    public sealed class RootDbDTO
    {
        public IEnumerable<PlayerDbDTO> Players { get; set; } = [];
    }

    public sealed class PlayerDbDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string Sex { get; set; }
        public CountryDbDTO Country { get; set; }
        public string Picture { get; set; }
        public PlayerDbDataDTO Data { get; set; }
    }

    public sealed class CountryDbDTO
    {
        public string Picture { get; set; }
        public string Code { get; set; }
    }

    public sealed class PlayerDbDataDTO
    {
        public int Rank { get; set; }
        public int Points { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public IEnumerable<int> Last { get; set; } = [];
    }
}
