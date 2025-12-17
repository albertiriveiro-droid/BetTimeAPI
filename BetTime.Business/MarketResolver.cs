using BetTime.Models;

namespace BetTime.Business;


public static class MarketResolver
{
    public static bool Resolve(Match match, MarketSelection selection)
    {
        if (selection.Market == null)
            throw new ArgumentNullException(nameof(selection.Market), "Market cannot be null for this selection.");

        switch (selection.Market.MarketType)
        {
            case MarketType.OneXTwo:
                var outcome = match.HomeScore > match.AwayScore ? OneXTwoSelection.Home :
                              match.HomeScore < match.AwayScore ? OneXTwoSelection.Away :
                              OneXTwoSelection.Draw;

                if (!Enum.TryParse<OneXTwoSelection>(selection.Name, out var sel))
                    throw new ArgumentException($"Invalid selection {selection.Name} for OneXTwo market.");

                return sel == outcome;

            case MarketType.OverUnderGoals:
                var totalGoals = match.HomeScore + match.AwayScore;
                var threshold = selection.Threshold ?? 2.5m;

                if (!Enum.TryParse<OverUnderSelection>(selection.Name, out var ouSel))
                    throw new ArgumentException($"Invalid selection {selection.Name} for Over/Under market.");

                return ouSel switch
                {
                    OverUnderSelection.Over => totalGoals > threshold,
                    OverUnderSelection.Under => totalGoals <= threshold,
                    _ => throw new ArgumentOutOfRangeException()
                };

            case MarketType.TotalCorners:
                var totalCorners = match.HomeCorners + match.AwayCorners;
                var cornerThreshold = selection.Threshold ?? 10;

                if (!Enum.TryParse<TotalCornersSelection>(selection.Name, out var cornerSel))
                    throw new ArgumentException($"Invalid selection {selection.Name} for Total Corners market.");

                return cornerSel switch
                {
                    TotalCornersSelection.Over => totalCorners > cornerThreshold,
                    TotalCornersSelection.Under => totalCorners <= cornerThreshold,
                    _ => throw new ArgumentOutOfRangeException()
                };
                case MarketType.BothToScore:
                {
                bool bothScored = match.HomeScore > 0 && match.AwayScore > 0;

                 if (!Enum.TryParse<BothToScoreSelection>(selection.Name, out var bttsSel))
                 throw new ArgumentException(
                $"Invalid selection {selection.Name} for BothToScore market."
                 );

                 return bttsSel switch
                {
                 BothToScoreSelection.Yes => bothScored,
                BothToScoreSelection.No => !bothScored,
                _ => throw new ArgumentOutOfRangeException()
                };
                }

            default:
                throw new NotImplementedException($"MarketType {selection.Market.MarketType} not implemented");
        }
    }
}