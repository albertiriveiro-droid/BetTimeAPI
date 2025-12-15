using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Data
{
    public class PlayerMarketSelectionEFRepository : IPlayerMarketSelectionRepository
    {
        private readonly BetTimeContext _context;

        public PlayerMarketSelectionEFRepository(BetTimeContext context)
        {
            _context = context;
        }

        public void AddPlayerSelection(PlayerMarketSelection selection)
        {
            _context.PlayerMarketSelections.Add(selection);
            SaveChanges();
        }



        public PlayerMarketSelection? GetSelectionByPlayerId(int id)
        {
            return _context.PlayerMarketSelections.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<PlayerMarketSelection> GetSelectionsByPlayerMarket(int playerMarketId)
        {
            return _context.PlayerMarketSelections
                .Where(s => s.PlayerMarketId == playerMarketId)
                .ToList();
        }

        public PlayerMarketSelection GetSelectionById(int selectionId)
        {
         return _context.PlayerMarketSelections
                   .Include(s => s.PlayerMarket) // muy importante incluir el PlayerMarket
                   .FirstOrDefault(s => s.Id == selectionId);
}


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
