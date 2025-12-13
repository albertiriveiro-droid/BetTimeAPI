using BetTime.Models;

namespace BetTime.Data;

public interface IMarketRepository
{
    Market AddMarket(Market market);
    Market? GetMarketById(int marketId);
    IEnumerable<Market> GetMarketByMatch(int matchId);
}