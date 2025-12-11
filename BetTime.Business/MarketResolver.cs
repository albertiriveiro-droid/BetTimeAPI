using BetTime.Models;

namespace BetTime.Business;

public static class MarketResolver
{
    public static bool Resolve(Match match, MarketSelection selection)
    {
        if (selection.Market == null)
            throw new ArgumentNullException(nameof(selection.Market), "Market cannot be null for this selection.");

        var market = selection.Market;
        string selName = selection.Name.Trim().ToUpper();

        switch (market.MarketType.ToUpper())
        {
            case "1X2":
                string outcome = match.HomeScore > match.AwayScore ? "HOME" :
                                 match.HomeScore < match.AwayScore ? "AWAY" : "DRAW";
                return selName == outcome;

            case "OVER/UNDER 2.5":
                decimal totalGoals = match.HomeScore + match.AwayScore;
                decimal threshold = selection.Threshold ?? 2.5m;
                if (selName.Contains("OVER"))
                    return totalGoals > threshold;
                else if (selName.Contains("UNDER"))
                    return totalGoals <= threshold;
                else
                    throw new ArgumentException($"Invalid selection name {selection.Name} for Over/Under market.");

            case "TOTAL CORNERS":
                int totalCorners = match.HomeCorners + match.AwayCorners;
                int cornerThreshold = selection.Threshold.HasValue ? (int)selection.Threshold.Value : 10;
                if (selName.Contains("OVER"))
                    return totalCorners > cornerThreshold;
                else if (selName.Contains("UNDER"))
                    return totalCorners <= cornerThreshold;
                else
                    throw new ArgumentException($"Invalid selection name {selection.Name} for Total Corners market.");

            default:
                throw new NotImplementedException($"MarketType {market.MarketType} not implemented");
        }
    }
}