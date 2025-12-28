using Microsoft.EntityFrameworkCore;

using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Data;

public class BetEFRepository : IBetRepository
{
    private readonly BetTimeContext _context;

    public BetEFRepository(BetTimeContext context)
    {
        _context = context;
    }

    public void AddBet(Bet bet)
    {
        _context.Bets.Add(bet);
        SaveChanges();
    }

    public IEnumerable<Bet> GetAllBets()
    {
        return _context.Bets
            .Include(b => b.User)
            .Include(b => b.MarketSelection)
            .Include(b => b.PlayerMarketSelection)
                .ThenInclude(pms => pms.PlayerMarket)
          
            .Include(b => b.Match)
                .ThenInclude(m => m.Markets)
                    .ThenInclude(mk => mk.Selections)
            .Include(b => b.Match)
                .ThenInclude(m => m.PlayerMarkets)
                    .ThenInclude(pm => pm.Selections)
            .ToList();
    }

    public Bet? GetBetById(int betId)
{
    return _context.Bets
        .Include(b => b.User)

        .Include(b => b.MarketSelection)
            .ThenInclude(ms => ms.Market)   

        .Include(b => b.PlayerMarketSelection)
            .ThenInclude(pms => pms.PlayerMarket)

        .Include(b => b.Match)
            .ThenInclude(m => m.Markets)
                .ThenInclude(mk => mk.Selections)

        .Include(b => b.Match)
            .ThenInclude(m => m.PlayerMarkets)
                .ThenInclude(pm => pm.Selections)

        .FirstOrDefault(b => b.Id == betId);
}

    public IEnumerable<Bet> GetBetsByUser(int userId)
    {
        return _context.Bets
            .Where(b => b.UserId == userId)
            .Include(b => b.MarketSelection)
            .Include(b => b.PlayerMarketSelection)
                .ThenInclude(pms => pms.PlayerMarket)
         
            .Include(b => b.Match)
                .ThenInclude(m => m.Markets)
                    .ThenInclude(mk => mk.Selections)
            .Include(b => b.Match)
                .ThenInclude(m => m.PlayerMarkets)
                    .ThenInclude(pm => pm.Selections)
            .ToList();
    }

    public IEnumerable<Bet> GetBetsByMatch(int matchId)
{
    return _context.Bets
        .Where(b => b.MatchId == matchId)

        .Include(b => b.User)

        .Include(b => b.MarketSelection)
            .ThenInclude(ms => ms.Market)   

        .Include(b => b.PlayerMarketSelection)
            .ThenInclude(pms => pms.PlayerMarket)

        .Include(b => b.Match)
            .ThenInclude(m => m.Markets)
                .ThenInclude(mk => mk.Selections)

        .Include(b => b.Match)
            .ThenInclude(m => m.PlayerMarkets)
                .ThenInclude(pm => pm.Selections)

        .ToList();
}
    public IEnumerable<Bet> GetActiveBets()
    {
        return _context.Bets
            .Where(b => b.Won == null)
            .Include(b => b.MarketSelection)
            .ThenInclude(ms => ms.Market)
            .Include(b => b.PlayerMarketSelection)
            
            .ThenInclude(pms => pms.PlayerMarket)
          
            .Include(b => b.Match)
                .ThenInclude(m => m.Markets)
                    .ThenInclude(mk => mk.Selections)
            .Include(b => b.Match)
                .ThenInclude(m => m.PlayerMarkets)
                    .ThenInclude(pm => pm.Selections)
            .ToList();
    }

    public IEnumerable<Bet> GetFinishedBets()
    {
        return _context.Bets
            .Where(b => b.Won != null)
            .Include(b => b.MarketSelection)
            .ThenInclude(ms => ms.Market)
            .Include(b => b.PlayerMarketSelection)
                .ThenInclude(pms => pms.PlayerMarket)
           
            .Include(b => b.Match)
                .ThenInclude(m => m.Markets)
                    .ThenInclude(mk => mk.Selections)
            .Include(b => b.Match)
                .ThenInclude(m => m.PlayerMarkets)
                    .ThenInclude(pm => pm.Selections)
            .ToList();
    }

    public IEnumerable<Bet> GetWonBets(int userId)
    {
        return _context.Bets
            .Where(b => b.UserId == userId && b.Won == true)
            .Include(b => b.MarketSelection)
            .ThenInclude(ms => ms.Market)
            .Include(b => b.PlayerMarketSelection)
                .ThenInclude(pms => pms.PlayerMarket)
           
            .Include(b => b.Match)
                .ThenInclude(m => m.Markets)
                    .ThenInclude(mk => mk.Selections)
            .Include(b => b.Match)
                .ThenInclude(m => m.PlayerMarkets)
                    .ThenInclude(pm => pm.Selections)
            .ToList();
    }

    public IEnumerable<Bet> GetLostBets(int userId)
    {
        return _context.Bets
            .Where(b => b.UserId == userId && b.Won == false)
            .Include(b => b.MarketSelection)
            .Include(b => b.PlayerMarketSelection)
                .ThenInclude(pms => pms.PlayerMarket)
          
            .Include(b => b.Match)
                .ThenInclude(m => m.Markets)
                    .ThenInclude(mk => mk.Selections)
            .Include(b => b.Match)
                .ThenInclude(m => m.PlayerMarkets)
                    .ThenInclude(pm => pm.Selections)
            .ToList();
    }

    public void UpdateBet(Bet bet)
    {
        _context.Entry(bet).State = EntityState.Modified;
        SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}