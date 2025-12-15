using System;
using System.Text.Json.Serialization;

namespace BetTime.Models
{
    public class PlayerMarketSelection
    {
        public int Id { get; set; }
        public int PlayerMarketId { get; set; }
        public string Name { get; set; }
        public decimal Odd { get; set; }
        public decimal? Threshold { get; set; }
        [JsonIgnore]
        public PlayerMarket PlayerMarket { get; set; }

        
        public PlayerMarketSelection() { }

        // Constructor normal
        public PlayerMarketSelection(int playerMarketId, string name, decimal odd, decimal? threshold = null)
        {
            PlayerMarketId = playerMarketId;
            Name = name;
            Odd = odd;
            Threshold = threshold;
        }
    }
}