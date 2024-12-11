namespace Domain.Entities
{
    public sealed class PlayersDirectory
    {
        private readonly IEnumerable<Player> _players;
        
        public PlayersDirectory(IEnumerable<Player> players)
        {
            if (players == null || !players.Any())
                throw new InvalidOperationException("Players list cannot be null or empty!");

            _players = players;
        }

        public double CalculateAverageBodyMassIndex()
        {
            var avgBMI = _players.Average(x => x.CalculateBodyMassIndex());
            return Math.Round(avgBMI, 2, MidpointRounding.ToEven);
        }

        public Country FindCountryWithHighestWinningPoints()
        {
            return _players.
                GroupBy(x => x.Country).
                Select(g => new
                {
                    Country = g.Key,
                    TotalPoints = g.Sum(p => p.Data.Points)
                }).
                OrderByDescending(c => c.TotalPoints).
                First().
                Country;
        }

        public double CalculateMedianHeightForAllPlayers()
        {
            var orderedHeights = _players
                .Select(p => p.Data.Height)
                .OrderBy(h => h)
                .ToList();

            var playersCount = orderedHeights.Count;

            if (playersCount % 2 == 1)
            {
                return orderedHeights[playersCount / 2];
            }

            var m1 = orderedHeights[playersCount / 2 - 1];
            var m2 = orderedHeights[playersCount / 2];

            return Math.Round(( m1 + m2) / 2, 2, MidpointRounding.ToEven);
        }
    }
}
