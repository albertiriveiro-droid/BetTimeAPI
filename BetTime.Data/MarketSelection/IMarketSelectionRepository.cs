using BetTime.Models;

namespace BetTime.Data;

public interface IMarketSelectionRepository
{
    MarketSelection AddMarketSelection(MarketSelection selection);
    MarketSelection? GetSelectionById(int selectionId);
    IEnumerable<MarketSelection> GetSelectionsByMarket(int marketId);

}