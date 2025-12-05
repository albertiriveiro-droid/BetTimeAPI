namespace BetTime.Models;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int SportId { get; set; }
    public Sport? Sport { get; set; }

    public ICollection<Team> Teams { get; set; }
    public ICollection<Match> Matches { get; set; }

  
    public League(){}

    public League(string name, int sportId)
    {
        Name = name;
        SportId = sportId;
        Teams = new List<Team>();
        Matches = new List<Match>();
    }
}