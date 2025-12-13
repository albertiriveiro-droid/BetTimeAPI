using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Data;

public class MarketSelectionEFRepository : IMarketSelectionRepository
{
    private readonly BetTimeContext _context;

    public MarketSelectionEFRepository(BetTimeContext context)
    {
        _context = context;
    }

    public MarketSelection AddMarketSelection(MarketSelection selection)
    {
        _context.MarketSelections.Add(selection);
        _context.SaveChanges();
        return selection;
    }

    public MarketSelection? GetSelectionById(int selectionId)
    {
        return _context.MarketSelections
            .Include(s => s.Market)
                .ThenInclude(m => m.Match)
            .FirstOrDefault(s => s.Id == selectionId);
    }

    public IEnumerable<MarketSelection> GetSelectionsByMarket(int marketId)
    {
        return _context.MarketSelections
            .Where(s => s.MarketId == marketId)
            .ToList();
    }

 
}