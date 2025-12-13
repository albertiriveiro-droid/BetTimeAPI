using BetTime.Models;
using BetTime.Data;

namespace BetTime.Business;

public class MarketSelectionService : IMarketSelectionService
{
    private readonly IMarketRepository _marketRepository;
    private readonly IMarketSelectionRepository _selectionRepository;

    public MarketSelectionService(
        IMarketRepository marketRepository,
        IMarketSelectionRepository selectionRepository)
    {
        _marketRepository = marketRepository;
        _selectionRepository = selectionRepository;
    }

 public MarketSelection CreateSelection(int marketId, MarketSelectionCreateDTO dto)
{
    var market = _marketRepository.GetMarketById(marketId)
        ?? throw new KeyNotFoundException("Market not found");

    if (market.Match!.Finished)
        throw new InvalidOperationException("Cannot add selections to finished match");


    switch (market.MarketType)
    {
        case MarketType.OneXTwo:
            if (!Enum.IsDefined(typeof(OneXTwoSelection), dto.Name))
                throw new InvalidOperationException("Invalid selection for 1X2 market");
            break;

        case MarketType.OverUnderGoals:
            if (!Enum.IsDefined(typeof(OverUnderSelection), dto.Name))
                throw new InvalidOperationException("Invalid selection for Over/Under market");
            break;

        case MarketType.TotalCorners:
            if (!Enum.IsDefined(typeof(TotalCornersSelection), dto.Name))
                throw new InvalidOperationException("Invalid selection for Total Corners market");
            break;

        default:
            throw new NotImplementedException("Market type not supported");
    }

    var selection = new MarketSelection
    {
        MarketId = marketId,
        Name = dto.Name,
        Odd = dto.Odd,
        Threshold = dto.Threshold
    };

    _selectionRepository.AddMarketSelection(selection);
    return selection;
}
    public IEnumerable<MarketSelection> GetSelectionsByMarket(int marketId)
    {
        var market = _marketRepository.GetMarketById(marketId)
            ?? throw new KeyNotFoundException($"Market with ID {marketId} not found");

        return _selectionRepository.GetSelectionsByMarket(marketId);
    }

    public MarketSelection GetSelectionById(int selectionId)
    {
        return _selectionRepository.GetSelectionById(selectionId)
            ?? throw new KeyNotFoundException($"Selection with ID {selectionId} not found");
    }
}
