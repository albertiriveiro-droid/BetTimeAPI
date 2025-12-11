using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
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


   
    public int HomeScore { get; set; } = 0;

    public int HomeCorners {get; set;} = 0;
    public int AwayCorners {get; set;}= 0;
    public int AwayScore { get; set; } = 0;
    [Required]
    public int DurationMinutes { get; set; } = 90;
   
    public double HomeWinProbability { get; set; }
    public double DrawProbability { get; set; }
    public double AwayWinProbability { get; set; }

    [Required]
    public bool Finished { get; set; }

    public ICollection<Bet> Bets { get; set; } = new List<Bet>();
    public ICollection<Market> Markets { get; set; } = new List<Market>();

    public Match() {}

    public Match(int leagueId, int homeTeamId, int awayTeamId, DateTime startTime, int durationMinutes)
    {
        LeagueId = leagueId;
        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
        StartTime = startTime;
        
        DurationMinutes=durationMinutes;
        Finished = false;
        Bets = new List<Bet>();
        Markets= new List<Market>();
    }
}