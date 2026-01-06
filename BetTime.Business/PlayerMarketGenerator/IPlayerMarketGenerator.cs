using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Business;

public interface IPlayerMarketGeneratorService
{
    List<PlayerMarket> GenerateMarketsForPlayer(int matchId, int playerId);
}