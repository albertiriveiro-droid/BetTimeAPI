using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Data;

public class MarketEFRepository : IMarketRepository
{
    private readonly BetTimeContext _context;

    public MarketEFRepository(BetTimeContext context)
    {
        _context = context;
    }

    public Market AddMarket(Market market)
    {
        _context.Markets.Add(market);
        _context.SaveChanges();
        return market;
    }

    public Market? GetMarketById(int marketId)
    {
        return _context.Markets
            .Include(m => m.Match)
            .Include(m => m.Selections)
            .FirstOrDefault(m => m.Id == marketId);
    }

    public IEnumerable<Market> GetMarketByMatch(int matchId)
    {
        return _context.Markets
            .Where(m => m.MatchId == matchId)
            .Include(m => m.Selections)
            .ToList();
    }

}
