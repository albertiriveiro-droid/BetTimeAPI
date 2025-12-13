using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;
public class MarketCreateDTO
{
    [Required]
    public MarketType MarketType{ get; set; } 

    public string? Description { get; set; }

}
