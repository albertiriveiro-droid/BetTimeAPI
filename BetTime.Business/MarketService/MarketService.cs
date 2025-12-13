using BetTime.Models;
using BetTime.Data;

namespace BetTime.Business;

public class MarketService : IMarketService
{
    private readonly IMarketRepository _marketRepository;
    private readonly IMatchRepository _matchRepository;

    public MarketService(
        IMarketRepository marketRepository,
        IMatchRepository matchRepository)
    {
        _marketRepository = marketRepository;
        _matchRepository = matchRepository;
    }

    public Market CreateMarket(int matchId, MarketCreateDTO dto)
    {
        var match = _matchRepository.GetMatchById(matchId)
            ?? throw new KeyNotFoundException("Match not found");

        if (match.Finished)
            throw new InvalidOperationException("Cannot create markets for finished match");

        if (match.StartTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot create markets after match start");

        var market = new Market(matchId, dto.MarketType, dto.Description);

        return _marketRepository.AddMarket(market);
    }

    

    public IEnumerable<Market> GetMarketsByMatch(int matchId)
        => _marketRepository.GetMarketByMatch(matchId);

    public Market GetMarketById(int marketId)
        => _marketRepository.GetMarketById(marketId)
            ?? throw new KeyNotFoundException("Market not found");
    
}