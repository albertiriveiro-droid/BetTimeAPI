using BetTime.Models;
using System.Collections.Generic;

public interface IMarketSelectionService
{
   
    MarketSelection CreateSelection(
        int marketId,
        MarketSelectionCreateDTO selectionDTO
    );

   
    IEnumerable<MarketSelection> GetSelectionsByMarket(int marketId);

  
    MarketSelection GetSelectionById(int selectionId);

  
}