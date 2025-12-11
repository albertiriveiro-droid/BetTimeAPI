using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BetTime.Models;

public class MarketSelection
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MarketId { get; set; }
    [JsonIgnore]
    public Market? Market { get; set; }

    [Required]
    public string Name { get; set; } = ""; 

    [Required]
    public decimal Odd { get; set; } 
    
    public decimal? Threshold { get; set; } 


    public MarketSelection() {}

    public MarketSelection(int marketId, string name, decimal odd, decimal? threshold = null)
    {
        MarketId = marketId;
        Name = name;
        Odd = odd;
        Threshold = threshold;
    }
}