 using BetTime.Models;
 namespace BetTime.Data;
 
 public interface IPlayerMarketRepository
    {
        void AddPlayerMarket(PlayerMarket market);
        PlayerMarket? GetPlayerMarketById(int id);
        IEnumerable<PlayerMarket> GetPlayerMarketsByMatch(int matchId);
        void SaveChanges();
    }
