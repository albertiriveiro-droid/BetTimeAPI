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

                MarketType.OverUnderGoals =>
                    GenerateOverUnderOdd(threshold, side == "Over", isCorners: false),

                MarketType.TotalCorners =>
                    GenerateOverUnderOdd(threshold, side == "Over", isCorners: true),

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

       
        private decimal GenerateOverUnderOdd(
            decimal threshold,
            bool isOver,
            bool isCorners)
        {
            decimal baseProbability;

            if (!isCorners)
            {
               
                baseProbability = threshold switch
                {
                    <= 1.5m => isOver ? 0.72m : 0.28m,
                    <= 2.5m => isOver ? 0.55m : 0.45m,
                    <= 3.5m => isOver ? 0.32m : 0.68m,
                    <= 4.5m => isOver ? 0.18m : 0.82m,
                    _       => isOver ? 0.10m : 0.90m
                };
            }
            else
            {
             
                baseProbability = threshold switch
                {
                    <= 8.5m  => isOver ? 0.65m : 0.35m,
                    <= 9.5m  => isOver ? 0.55m : 0.45m,
                    <= 10.5m => isOver ? 0.42m : 0.58m,
                    <= 11.5m => isOver ? 0.30m : 0.70m,
                    <= 12.5m => isOver ? 0.20m : 0.80m
                };
            }

          
            var variation = (decimal)(_rnd.NextDouble() * 0.06 - 0.03);
            var finalProbability = Math.Clamp(baseProbability + variation, 0.05m, 0.95m);

            return Math.Round(1 / finalProbability, 2);
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
