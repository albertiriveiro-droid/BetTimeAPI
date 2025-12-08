namespace BetTime.Models;
public class BetOutputDTO
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public string Prediction { get; set; } = null!;
    public decimal AmountBet { get; set; }
    public decimal Odds { get; set; }
    public bool? Won { get; set; }
    public decimal? AmountWon { get; set; }
    public DateTime Date { get; set; }
}