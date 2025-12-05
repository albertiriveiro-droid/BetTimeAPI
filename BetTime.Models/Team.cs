namespace BetTime.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int LeagueId { get; set; }
    public League? League { get; set; }

    public ICollection<Match> HomeMatches { get; set; }

    public ICollection<Match> AwayMatches { get; set; }

    public Team(){}

    public Team(string name, int leagueId)
    {
        Name = name;
        LeagueId = leagueId;
        HomeMatches = new List<Match>();
        AwayMatches = new List<Match>();
    }
}