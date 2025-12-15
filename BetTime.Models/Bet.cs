using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BetTime.Models;
public class Bet
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

    [ForeignKey("Match")]
    public int MatchId { get; set; }
    [JsonIgnore]
    public Match? Match { get; set; }

    
    [ForeignKey("MarketSelection")]
    public int? MarketSelectionId { get; set; }
    [JsonIgnore]
    public MarketSelection? MarketSelection { get; set; }


    [ForeignKey("PlayerMarketSelection")]
    public int? PlayerMarketSelectionId { get; set; }
    [JsonIgnore]
    public PlayerMarketSelection? PlayerMarketSelection { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    public bool? Won { get; set; }
    public DateTime PlacedAt { get; set; }

    public Bet() {}

    public Bet(int userId, int matchId, int marketSelectionId, int playerMarketSelectionId, decimal amount)
    {
        UserId = userId;
        MatchId = matchId;
        MarketSelectionId = marketSelectionId;
        PlayerMarketSelectionId= playerMarketSelectionId;
        Amount = amount;
        Won = null;
        PlacedAt = DateTime.UtcNow;
    }
}