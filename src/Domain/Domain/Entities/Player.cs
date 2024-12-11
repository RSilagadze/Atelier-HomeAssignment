namespace Domain.Entities
{
    public sealed class Player(
        int id,
        string firstname,
        string lastname,
        string shortname,
        string sex,
        Country country,
        string picture,
        PlayerData data)
    {
        public int Id { get; } = id;
        public string Firstname { get; } = firstname;
        public string Lastname { get; } = lastname;
        public string Shortname { get; } = shortname;
        public string Sex { get; } = sex;
        public Country Country { get; } = country;
        public string Picture { get; } = picture;
        public PlayerData Data { get; } = data;

        public double CalculateBodyMassIndex()
        {
            if (Data.Weight <= 0)
                throw new InvalidOperationException("Weight cannot be below or zero.");
            if (Data.Height <= 0)
                throw new InvalidOperationException("Height cannot be below or zero.");

            var weightKg = Data.Weight / 1000;
            var heightM = Data.Height / 100;
            return Math.Round(weightKg / (heightM * heightM), 2, MidpointRounding.ToEven);
        }
    }
}
