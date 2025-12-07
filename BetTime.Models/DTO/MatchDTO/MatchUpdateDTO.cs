namespace BetTime.Models;

public class MatchUpdateDTO
{
    public DateTime? StartTime { get; set; }

    public decimal? HomeOdds { get; set; }
    public decimal? DrawOdds { get; set; }
    public decimal? AwayOdds { get; set; }

    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public bool? Finished { get; set; }
}