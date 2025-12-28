namespace BetTime.Models;

public class PlayerMarketSelectionOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Odd { get; set; }
        public decimal? Threshold { get; set; }
    }