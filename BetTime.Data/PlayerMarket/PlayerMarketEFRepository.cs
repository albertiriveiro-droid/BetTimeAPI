using BetTime.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BetTime.Data
{
    public class PlayerMarketEFRepository : IPlayerMarketRepository
    {
        private readonly BetTimeContext _context;

        public PlayerMarketEFRepository(BetTimeContext context)
        {
            _context = context;
        }

        public void AddPlayerMarket(PlayerMarket market)
        {
            _context.PlayerMarkets.Add(market);
            SaveChanges();
        }

        public PlayerMarket? GetPlayerMarketById(int id)
        {
            return _context.PlayerMarkets
                .Include(pm => pm.Selections)
                .FirstOrDefault(pm => pm.Id == id);
        }

        public IEnumerable<PlayerMarket> GetPlayerMarketsByMatch(int matchId)
        {
            return _context.PlayerMarkets
                .Where(pm => pm.MatchId == matchId)
                .Include(pm => pm.Selections)
                .ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}