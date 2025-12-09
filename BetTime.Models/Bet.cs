using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BetTime.Models;

public class Bet
{
[Key]   
public int Id {get; set;}
 [ForeignKey("User")]
public int UserId {get; set;}
[JsonIgnore]
public User? User { get; set; }
 [ForeignKey("Match")]
public int MatchId {get; set;}
[JsonIgnore]
public Match? Match { get; set; }

[Required]
[Column(TypeName = "decimal(10,2)")]
public decimal Amount { get; set; }
[Required]
[Column(TypeName = "decimal(10,2)")]
public decimal Odds { get; set; } 
[Required]
public string Prediction { get; set; }
public bool? Won { get; set; }
public DateTime PlacedAt { get; set; }


public Bet(){}


 public Bet(int userId, int matchId, decimal amount, decimal odds, string prediction)
    {
        UserId = userId;
        MatchId = matchId;
        Amount = amount;
        Odds = odds;
        Prediction = prediction;
        Won = null;
        PlacedAt = DateTime.UtcNow;
    }


}