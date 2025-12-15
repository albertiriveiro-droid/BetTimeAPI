using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BetTime.Models;

public class Team
{   [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [ForeignKey("League")]
    public int LeagueId { get; set; }
    [JsonIgnore]
    public League? League { get; set; }

    public ICollection<Player> Players { get; set; } 

    public ICollection<Match> HomeMatches { get; set; }

    public ICollection<Match> AwayMatches { get; set; }

    public Team(){}

    public Team(string name, int leagueId)
    {
        Name = name;
        LeagueId = leagueId;
        HomeMatches = new List<Match>();
        AwayMatches = new List<Match>();
        Players = new List<Player>();
    }
}