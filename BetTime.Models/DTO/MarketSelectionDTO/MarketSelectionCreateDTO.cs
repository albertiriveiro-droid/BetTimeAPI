using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;
public class MarketSelectionCreateDTO
{
    [Required]
    public string Name { get; set; } = "";  

    [Required]
    public decimal Odd { get; set; }  
}