namespace BetTime.Models;


public class Match
{
    public int Id { get; set; }
    public int LeagueId { get; set; }
    public League? League { get; set; }

    public int HomeTeamId { get; set; }
    public Team? HomeTeam { get; set; }

    public int AwayTeamId { get; set; }
    public Team? AwayTeam { get; set; }

    public DateTime StartTime { get; set; }

    public decimal HomeOdds { get; set; }
    public decimal DrawOdds { get; set; }
    public decimal AwayOdds { get; set; }
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public bool Finished { get; set; }

    public ICollection<Bet> Bets { get; set; }

    public Match(){}

    public Match(int leagueId, int homeTeamId, int awayTeamId, DateTime startTime, decimal homeOdds, decimal drawOdds, decimal awayOdds)
    {
        LeagueId = leagueId;
        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
        StartTime = startTime;
        HomeOdds= homeOdds;
        DrawOdds= drawOdds;
        AwayOdds= awayOdds;
        Finished = false;
        Bets = new List<Bet>();
    }
}