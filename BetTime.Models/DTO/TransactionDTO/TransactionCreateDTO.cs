using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;

public class TransactionCreateDTO
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public decimal Amount { get; set; }


    [Required]
    public string? Type { get; set; }

    public string? PaymentMethod { get; set; }
}