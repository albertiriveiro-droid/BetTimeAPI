namespace BetTime.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password{ get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }

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