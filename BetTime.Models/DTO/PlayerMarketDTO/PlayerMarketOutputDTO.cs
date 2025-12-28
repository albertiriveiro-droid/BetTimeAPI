namespace BetTime.Models;
    public class PlayerMarketOutputDTO
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = "";
        public string TeamName { get; set; } = "";
        public int MatchId { get; set; }
        public PlayerMarketType PlayerMarketType { get; set; }
        public bool IsOpen { get; set; }
        public List<PlayerMarketSelectionOutputDTO> Selections { get; set; } = new List<PlayerMarketSelectionOutputDTO>();
    }