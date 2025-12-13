using BetTime.Models;

public interface IMarketService
{
    Market CreateMarket(int matchId, MarketCreateDTO marketDTO);
    IEnumerable<Market> GetMarketsByMatch(int matchId);
    Market GetMarketById(int marketId);

    
}