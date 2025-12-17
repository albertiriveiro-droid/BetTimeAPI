using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BetTime.Models;

public class PlayerMatchStats
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PlayerId { get; set; }
    [JsonIgnore]
    public Player? Player { get; set; }

    [Required]
    public int MatchId { get; set; }
    [JsonIgnore]
    public Match? Match { get; set; }

    public int Goals { get; set; }
    public int Assists { get; set; }

     public int YellowCard { get; set; }
    public int RedCard { get; set; }
    
    public int MinutesPlayed { get; set; }

    public PlayerMatchStats() { }

    public PlayerMatchStats(int playerId, int matchId)
    {
        PlayerId = playerId;
        MatchId = matchId;
    }
}