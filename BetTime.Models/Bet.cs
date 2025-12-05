namespace BetTime.Models;

public class Bet
{
    
public int Id {get; set;}
public int UserId {get; set;}
public User? User { get; set; }
public int MatchId {get; set;}
public Match? Match { get; set; }
public decimal Amount { get; set; }
public decimal Odds { get; set; } 
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