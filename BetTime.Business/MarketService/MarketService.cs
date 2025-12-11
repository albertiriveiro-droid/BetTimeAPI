using BetTime.Models;
using BetTime.Data;

namespace BetTime.Business;

public class MarketService : IMarketService
{
    private readonly IMatchRepository _matchRepository;
  

    public MarketService(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
        
    }

    public Market CreateMarket(int matchId, MarketCreateDTO marketDTO)
    {
        var match = _matchRepository.GetMatchById(matchId)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        var market = new Market(match.Id, marketDTO.MarketType, marketDTO.Description);

        foreach (var selDTO in marketDTO.Selections)
        {
            var selection = new MarketSelection
            {
                Name = selDTO.Name,
                Odd = selDTO.Odd
            };
            market.Selections.Add(selection);
        }

        match.Markets.Add(market);
        _matchRepository.UpdateMatch(match);

        return market;
    }

    public IEnumerable<Market> GetMarketsByMatch(int matchId)
    {
        var match = _matchRepository.GetMatchById(matchId)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        return match.Markets;
    }

    public Market GetMarketById(int marketId)
    {
        var allMarkets = _matchRepository.GetAllMatches()
            .SelectMany(m => m.Markets)
            .ToList();

        var market = allMarkets.FirstOrDefault(m => m.Id == marketId)
            ?? throw new KeyNotFoundException($"Market with ID {marketId} not found");

        return market;
    }

    public MarketSelection AddSelection(int marketId, MarketSelectionCreateDTO selectionDTO)
    {
        var market = GetMarketById(marketId);

        var selection = new MarketSelection
        {
            Name = selectionDTO.Name,
            Odd = selectionDTO.Odd
        };

        market.Selections.Add(selection);
        _matchRepository.UpdateMatch(market.Match!);
        return selection;
    }

    public MarketSelection UpdateSelection(int selectionId, decimal newOdd, string? newName = null)
    {
        var allMarkets = _matchRepository.GetAllMatches()
            .SelectMany(m => m.Markets)
            .ToList();

        var selection = allMarkets
            .SelectMany(m => m.Selections)
            .FirstOrDefault(s => s.Id == selectionId)
            ?? throw new KeyNotFoundException($"Selection with ID {selectionId} not found");

        selection.Odd = newOdd;
        if (!string.IsNullOrEmpty(newName))
            selection.Name = newName;

        _matchRepository.UpdateMatch(selection.Market!.Match!);
        return selection;
    }
}