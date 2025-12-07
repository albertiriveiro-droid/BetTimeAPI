namespace BetTime.Models;

public class TransactionDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!;
    public DateTime Date { get; set; }

    public string? PaymentMethod {get; set;}
}