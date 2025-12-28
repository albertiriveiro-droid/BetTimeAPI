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
        public IEnumerable<PlayerMarketOutputDTO> GetPlayerMarketsByMatchWithPlayer(int matchId)
        {
         return _context.PlayerMarkets
        .Where(pm => pm.MatchId == matchId)
        .Include(pm => pm.Selections)
        .Include(pm => pm.Player) 
        .ThenInclude(p => p.Team) 
        .Select(pm => new PlayerMarketOutputDTO
        {
            Id = pm.Id,
            PlayerId = pm.PlayerId,
            PlayerName = pm.Player.Name,
            TeamName = pm.Player.Team.Name, 
            MatchId = pm.MatchId,
            PlayerMarketType = pm.PlayerMarketType,
            IsOpen = pm.IsOpen,
            Selections = pm.Selections.Select(s => new PlayerMarketSelectionOutputDTO
            {
                Id = s.Id,
                Name = s.Name,
                Odd = s.Odd,
                Threshold = s.Threshold
            }).ToList()
        })
        .ToList();
}

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}