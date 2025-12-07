using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;
public class BetCreateDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int MatchId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Prediction { get; set; }
}