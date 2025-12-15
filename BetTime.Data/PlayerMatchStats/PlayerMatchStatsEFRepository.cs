using BetTime.Models;
using Microsoft.EntityFrameworkCore;
namespace BetTime.Data;



public class PlayerMatchStatsEFRepository : IPlayerMatchStatsRepository
{
    private readonly BetTimeContext _context;

    public PlayerMatchStatsEFRepository(BetTimeContext context)
    {
        _context = context;
    }

    public PlayerMatchStats? GetPlayerMatchStatsByPlayerAndMatch(int playerId, int matchId)
    {
        return _context.PlayerMatchStats
            .FirstOrDefault(p => p.PlayerId == playerId && p.MatchId == matchId);
    }

    public IEnumerable<PlayerMatchStats> GetPlayerMatchStatsByMatch(int matchId)
    {
        return _context.PlayerMatchStats
            .Where(p => p.MatchId == matchId)
            .ToList();
    }

    public void AddPlayerMatchStats(PlayerMatchStats stats)
    {
        _context.PlayerMatchStats.Add(stats);
        SaveChanges();
    }

    public void UpdatePlayerMatchStats(PlayerMatchStats stats)
    {
        _context.Entry(stats).State = EntityState.Modified;
        SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}