using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BetTime.Models;


public class Match
{   
     [Key]
    public int Id { get; set; }
    [Required]
    [ForeignKey("League")]
    public int LeagueId { get; set; }
    [JsonIgnore] 
    public League? League { get; set; }
    [Required]
    [ForeignKey("HomeTeam")]
    public int HomeTeamId { get; set; }
    [JsonIgnore]
    public Team? HomeTeam { get; set; }
    [Required]
    [ForeignKey("AwayTeam")]
    public int AwayTeamId { get; set; }
     [JsonIgnore]
    public Team? AwayTeam { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal HomeOdds { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal DrawOdds { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal AwayOdds { get; set; }
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
     [Required]
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