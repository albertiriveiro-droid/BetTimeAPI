using BetTime.Models;

namespace BetTime.Business
{
    public class DynamicOddsService
    {
        private readonly Random _rnd = new Random();

      

        public decimal GenerateMarketOdd(
            MarketType marketType,
            string side,
            decimal threshold = 0,
            decimal? probability = null)
        {
            if (probability.HasValue)
            {
                var prob = Math.Clamp(probability.Value, 0.05m, 0.95m);
                return Math.Round(1 / prob, 2);
            }

            return marketType switch
            {
                MarketType.OneXTwo => side switch
                {
                    "Home" => RandomRange(1.8m, 2.4m),
                    "Draw" => RandomRange(2.8m, 3.3m),
                    "Away" => RandomRange(2.0m, 2.6m),
                    _ => 2.0m
                },

                MarketType.OverUnderGoals or MarketType.TotalCorners
                    => RandomRange(1.7m, 2.2m),

                MarketType.BothToScore => side == "Yes"
                    ? RandomRange(1.6m, 2.0m)
                    : RandomRange(1.8m, 2.3m),

                _ => 2.0m
            };
        }

     
        public decimal GeneratePlayerOdd(
            PlayerMarketType type,
            PlayerPosition position,
            decimal? probability = null)
        {
            if (probability.HasValue)
            {
                var prob = Math.Clamp(probability.Value, 0.05m, 0.95m);
                return Math.Round(1 / prob, 2);
            }

            return type switch
            {
                PlayerMarketType.Goal => position switch
                {
                    PlayerPosition.Forward => RandomRange(1.6m, 2.2m),
                    PlayerPosition.Midfielder => RandomRange(2.4m, 3.5m),
                    PlayerPosition.Defender => RandomRange(4.5m, 7.0m),
                    PlayerPosition.Goalkeeper => RandomRange(15m, 40m),
                    _ => 3.0m
                },

                PlayerMarketType.Assist => position switch
                {
                    PlayerPosition.Forward => RandomRange(2.0m, 3.0m),
                    PlayerPosition.Midfielder => RandomRange(1.8m, 2.6m),
                    PlayerPosition.Defender => RandomRange(4.0m, 6.0m),
                    PlayerPosition.Goalkeeper => RandomRange(20m, 50m),
                    _ => 3.0m
                },

                PlayerMarketType.YellowCard => position switch
                {
                    PlayerPosition.Defender => RandomRange(1.6m, 2.2m),
                    PlayerPosition.Midfielder => RandomRange(1.9m, 2.6m),
                    PlayerPosition.Forward => RandomRange(2.5m, 3.5m),
                    PlayerPosition.Goalkeeper => RandomRange(3.5m, 6.0m),
                    _ => 2.5m
                },

                PlayerMarketType.RedCard => position switch
                {
                    PlayerPosition.Defender => RandomRange(6.0m, 12.0m),
                    PlayerPosition.Midfielder => RandomRange(7.0m, 14.0m),
                    PlayerPosition.Forward => RandomRange(8.0m, 16.0m),
                    PlayerPosition.Goalkeeper => RandomRange(10.0m, 25.0m),
                    _ => 10.0m
                },

                _ => 2.0m
            };
        }

      

        private decimal RandomRange(decimal min, decimal max)
        {
            return Math.Round(
                min + (decimal)_rnd.NextDouble() * (max - min),
                2
            );
        }
    }
}
