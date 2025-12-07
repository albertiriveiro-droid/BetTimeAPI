namespace BetTime.Models;

public class BetDTO
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Match { get; set; }

    public decimal Amount { get; set; }
    public decimal Odds { get; set; }

    public string Prediction { get; set; }

    public bool? Won { get; set; }
    public DateTime PlacedAt { get; set; }
}