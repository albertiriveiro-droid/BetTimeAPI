using BetTime.Data;
using BetTime.Models;

namespace BetTime.Business;

public static class PlayerMarketResolver
{
    public static bool Resolve(Match match, PlayerMarketSelection selection, IPlayerRepository playerRepository)
    {
        if (selection.PlayerMarket == null)
            throw new ArgumentNullException(nameof(selection.PlayerMarket), "PlayerMarket cannot be null");

        var player = playerRepository.GetPlayerById(selection.PlayerMarket.PlayerId)
                     ?? throw new KeyNotFoundException($"Player with ID {selection.PlayerMarket.PlayerId} not found");

      
        switch (selection.PlayerMarket.PlayerMarketType)
        {
            case PlayerMarketType.Goal:
                var playerStats = match.PlayerMatchStats
                    .FirstOrDefault(p => p.PlayerId == player.Id && p.MatchId == match.Id);

                return playerStats != null && playerStats.Goals > 0;

            case PlayerMarketType.Assist:
                playerStats = match.PlayerMatchStats
                    .FirstOrDefault(p => p.PlayerId == player.Id && p.MatchId == match.Id);

                return playerStats != null && playerStats.Assists > 0;

          

            default:
                throw new NotImplementedException($"PlayerMarketType {selection.PlayerMarket.PlayerMarketType} not implemented");
        }
    }
}