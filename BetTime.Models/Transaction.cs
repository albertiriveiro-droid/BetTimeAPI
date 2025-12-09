using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BetTime.Models;


public class Transaction
{
    [Key]
    public int Id { get; set; }
     [ForeignKey("User")]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    [Required]
    public string Type { get; set; } 
    public string? PaymentMethod { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }


    public Transaction(){}
    
    public Transaction(int userId, decimal amount, string type,string paymentMethod, string? note = null)
    {
       UserId= userId;
       Amount= amount;
       Type= type;
       PaymentMethod= paymentMethod;
       Note= note; 
       Date = DateTime.UtcNow;
    }
}
