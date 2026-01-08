using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Business;

public class MarketGeneratorService : IMarketGeneratorService
{
    private readonly IMarketService _marketService;
    private readonly IMarketSelectionService _selectionService;
    private readonly DynamicOddsService _dynamicOddsService;

    public MarketGeneratorService(
        IMarketService marketService,
        IMarketSelectionService selectionService,
        DynamicOddsService dynamicOddsService)
    {
        _marketService = marketService;
        _selectionService = selectionService;
        _dynamicOddsService = dynamicOddsService;
    }

    public List<Market> GenerateMarketsForMatch(int matchId)
    {
        var createdMarkets = new List<Market>();

        
        var oneXTwoDto = new MarketCreateDTO { MarketType = MarketType.OneXTwo };
        var oneXTwoMarket = _marketService.CreateMarket(matchId, oneXTwoDto);
        createdMarkets.Add(oneXTwoMarket);

        var oneXTwoSelections = new List<MarketSelectionCreateDTO>
        {
            new() { Name = OneXTwoSelection.Home.ToString(), Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.OneXTwo, "Home") },
            new() { Name = OneXTwoSelection.Draw.ToString(), Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.OneXTwo, "Draw") },
            new() { Name = OneXTwoSelection.Away.ToString(), Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.OneXTwo, "Away") }
        };

        foreach (var sel in oneXTwoSelections)
            _selectionService.CreateSelection(oneXTwoMarket.Id, sel);

        
        var overUnderGoalsDto = new MarketCreateDTO { MarketType = MarketType.OverUnderGoals };
        var overUnderGoalsMarket = _marketService.CreateMarket(matchId, overUnderGoalsDto);
        createdMarkets.Add(overUnderGoalsMarket);

        var goalThresholds = new decimal[] { 1.5m, 2.5m, 3.5m, 4.5m };
        foreach (var t in goalThresholds)
        {
            _selectionService.CreateSelection(overUnderGoalsMarket.Id, new MarketSelectionCreateDTO
            {
                Name = OverUnderSelection.Over.ToString(),
                Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.OverUnderGoals, "Over", t),
                Threshold = t
            });

            _selectionService.CreateSelection(overUnderGoalsMarket.Id, new MarketSelectionCreateDTO
            {
                Name = OverUnderSelection.Under.ToString(),
                Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.OverUnderGoals, "Under", t),
                Threshold = t
            });
        }

      
        var totalCornersDto = new MarketCreateDTO { MarketType = MarketType.TotalCorners };
        var totalCornersMarket = _marketService.CreateMarket(matchId, totalCornersDto);
        createdMarkets.Add(totalCornersMarket);

        var cornerThresholds = new decimal[] { 8.5m, 9.5m, 10.5m, 11.5m, 12.5m };
        foreach (var t in cornerThresholds)
        {
            _selectionService.CreateSelection(totalCornersMarket.Id, new MarketSelectionCreateDTO
            {
                Name = TotalCornersSelection.Over.ToString(),
                Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.TotalCorners, "Over", t),
                Threshold = t
            });

            _selectionService.CreateSelection(totalCornersMarket.Id, new MarketSelectionCreateDTO
            {
                Name = TotalCornersSelection.Under.ToString(),
                Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.TotalCorners, "Under", t),
                Threshold = t
            });
        }

     
        var bttsDto = new MarketCreateDTO { MarketType = MarketType.BothToScore };
        var bttsMarket = _marketService.CreateMarket(matchId, bttsDto);
        createdMarkets.Add(bttsMarket);

        var bttsSelections = new List<MarketSelectionCreateDTO>
        {
            new() { Name = BothToScoreSelection.Yes.ToString(), Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.BothToScore, "Yes") },
            new() { Name = BothToScoreSelection.No.ToString(), Odd = _dynamicOddsService.GenerateMarketOdd(MarketType.BothToScore, "No") }
        };

        foreach (var sel in bttsSelections)
            _selectionService.CreateSelection(bttsMarket.Id, sel);

        return createdMarkets;
    }
}
