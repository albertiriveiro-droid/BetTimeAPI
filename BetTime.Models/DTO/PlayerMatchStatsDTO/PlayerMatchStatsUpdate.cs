namespace BetTime.Models;

public class PlayerMatchStatsUpdateDTO
{
    public int? Goals { get; set; }
    public int? Assists { get; set; }
    public int? YellowCards { get; set; }
    public int? RedCards { get; set; }
    public int? MinutesPlayed { get; set; }
}