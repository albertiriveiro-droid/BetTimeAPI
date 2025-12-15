namespace BetTime.Models;

public class PlayerMatchStatsDTO
{
    public int PlayerId { get; set; }
    public int MatchId { get; set; }
    public int Goals { get; set; }
    public int Assists { get; set; }
    public int MinutesPlayed { get; set; }
}