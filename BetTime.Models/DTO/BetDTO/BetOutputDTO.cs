namespace BetTime.Models;
public class BetOutputDTO
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public string MarketType { get; set; } = null!;
    public string SelectionName { get; set; } = null!;
    public decimal AmountBet { get; set; }
    public decimal Odds { get; set; }
    public bool? Won { get; set; }
    public decimal? AmountWon { get; set; }
    public DateTime Date { get; set; }
}