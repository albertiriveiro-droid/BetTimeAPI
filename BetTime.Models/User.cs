using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;

public class User
{  
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password{ get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    [Required]
    public string Role { get; set; } = Roles.User;

    public ICollection<Transaction>? Transactions { get; set; }
    public ICollection<Bet>? Bets { get; set; }

    public User(){}

    public User (string username, string email, string password)
    {
        Username= username;
        Email=email;
        Password= password;
        Balance= 0;
        CreatedAt= DateTime.UtcNow;
        Transactions = new List<Transaction>();
        Bets = new List<Bet>();
    }
}