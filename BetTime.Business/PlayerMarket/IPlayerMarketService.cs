using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Services
{
    public interface IPlayerMarketService
    {
        PlayerMarket CreatePlayerMarket(int matchId, PlayerMarketCreateDTO playerMarketCreateDTO);
        PlayerMarket? GetPlayerMarketById(int id);
        IEnumerable<PlayerMarket> GetPlayerMarketsByMatch(int matchId);
    }
}