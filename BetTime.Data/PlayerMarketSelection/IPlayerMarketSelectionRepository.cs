
using BetTime.Models;
  namespace BetTime.Data;
  
  
  
  public interface IPlayerMarketSelectionRepository
    {
        void AddPlayerSelection(PlayerMarketSelection selection);
        PlayerMarketSelection? GetSelectionByPlayerId(int id);
        PlayerMarketSelection GetSelectionById(int selectionId);
        IEnumerable<PlayerMarketSelection> GetSelectionsByPlayerMarket(int playerMarketId);
        void SaveChanges();
    }
