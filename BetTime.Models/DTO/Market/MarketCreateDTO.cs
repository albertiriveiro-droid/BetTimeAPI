using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;
public class MarketCreateDTO
{
    [Required]
    public string MarketType { get; set; } = "";  

    public string? Description { get; set; }

    [Required]
    public List<MarketSelectionCreateDTO> Selections { get; set; } = new();
}
