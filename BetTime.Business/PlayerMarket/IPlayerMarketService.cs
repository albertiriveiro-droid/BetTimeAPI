using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Business
{
    public interface IPlayerMarketService
    {
        PlayerMarket CreatePlayerMarket(int matchId, PlayerMarketCreateDTO playerMarketCreateDTO);
        PlayerMarket? GetPlayerMarketById(int id);
        IEnumerable<PlayerMarket> GetPlayerMarketsByMatch(int matchId);
        IEnumerable<PlayerMarketOutputDTO> GetPlayerMarketsWithPlayerByMatch(int matchId);
    }
}