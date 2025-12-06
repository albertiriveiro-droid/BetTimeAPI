using BetTime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BetTime.Data;

public class LeagueEFRepository : ILeagueRepository
{
    private readonly BetTimeContext _context;

    public LeagueEFRepository(BetTimeContext context)
    {
       _context= context; 
    }

    public void AddLeague(League league)
    {
    _context.Leagues.Add(league);
    SaveChanges();
    }

    public IEnumerable<League> GetAllLeagues()
    {
        var leagues= _context.Leagues;
        return leagues;
    }

     public IEnumerable<League> GetLeaguesBySport(int sportId)
        {
            return _context.Leagues
                .Where(l => l.SportId == sportId)
                .Include(l => l.Sport)
                .ToList();
        }

   public League GetLeagueById(int LeagueId)
{
    var league = _context.Leagues
        .Include(l => l.Sport)
        .FirstOrDefault(l => l.Id == LeagueId);

    return league;
}

    public void DeleteLeague(League leagueDelete)
    {
      var league= GetLeagueById(leagueDelete.Id);
      _context.Leagues.Remove(league);
      SaveChanges();  
    }

    public void UpdateLeague( League league)
    {
    _context.Entry(league).State = EntityState.Modified;
    SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}