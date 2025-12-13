using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BetTime.Models;

public class Market
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MatchId { get; set; }
    [JsonIgnore]
    public Match? Match { get; set; }

    [Required]
    public MarketType MarketType { get; set; }  
    

    public string? Description { get; set; }

    public ICollection<MarketSelection> Selections { get; set; }

    public Market() {}

    public Market(int matchId, MarketType marketType, string? description = null)
    {
        MatchId = matchId;
        MarketType = marketType;
        Description = description;
        Selections = new List<MarketSelection>();
    }
}