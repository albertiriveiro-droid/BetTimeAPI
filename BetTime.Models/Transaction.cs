namespace BetTime.Models;


public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public decimal Amount { get; set; }
    public string Type { get; set; } 
    public DateTime Date { get; set; }
    public string? Note { get; set; }


    public Transaction(){}
    
    public Transaction(int userId, decimal amount, string type, string? note = null)
    {
       UserId= userId;
       Amount= amount;
       Type= type;
       Note= note; 
       Date = DateTime.UtcNow;
    }
}
