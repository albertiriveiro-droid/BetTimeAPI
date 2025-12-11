using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;
public class BetCreateDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int MarketSelectionId { get; set; } 

    [Required]
    public decimal Amount { get; set; }
}