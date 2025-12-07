
namespace BetTime.Models;

public class MatchDTO
{
    public int Id { get; set; }
    public string League { get; set; }

    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }

    public DateTime StartTime { get; set; }

    public decimal HomeOdds { get; set; }
    public decimal DrawOdds { get; set; }
    public decimal AwayOdds { get; set; }

    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public bool Finished { get; set; }
}