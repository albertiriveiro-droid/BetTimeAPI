using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Data;

public class MatchEFRepository : IMatchRepository
{
    private readonly BetTimeContext _context;

    public MatchEFRepository(BetTimeContext context)
    {
        _context = context;
    }

    public void AddMatch(Match match)
    {
        _context.Matches.Add(match);
        SaveChanges();
    }

    public IEnumerable<Match> GetAllMatches()
{
    return _context.Matches
        .Include(m => m.League)
        .Include(m => m.HomeTeam)
        .Include(m => m.AwayTeam)
        .Include(m => m.Markets)
            .ThenInclude(mk => mk.Selections)
        .Include(m => m.Bets)  
        .ToList();
}
    public Match? GetMatchById(int matchId)
    {
        return _context.Matches
            .Include(m => m.League)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Markets)
                .ThenInclude(mk => mk.Selections)
            .FirstOrDefault(m => m.Id == matchId);
    }

    public IEnumerable<Match> GetMatchesByLeague(int leagueId)
    {
        return _context.Matches
            .Where(m => m.LeagueId == leagueId)
            .Include(m => m.League)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Markets)
                .ThenInclude(mk => mk.Selections)
            .ToList();
    }

    public IEnumerable<Match> GetMatchesByTeam(int teamId)
    {
        return _context.Matches
            .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
            .Include(m => m.League)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Markets)
            .ThenInclude(mk => mk.Selections)
            .ToList();
    }

    public IEnumerable<Match> GetUpcomingMatches()
    {
        return _context.Matches
            .Where(m => m.StartTime > DateTime.UtcNow && !m.Finished)
            .Include(m => m.League)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Markets)
            .ThenInclude(mk => mk.Selections)
            .ToList();
    }

    public IEnumerable<Match> GetFinishedMatches()
    {
        return _context.Matches
            .Where(m => m.Finished)
            .Include(m => m.League)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Markets)
                .ThenInclude(mk => mk.Selections)
            .ToList();
    }

    public IEnumerable<Match> GetPendingMatches(DateTime currentTime)
    {
        return _context.Matches
            .Where(m => !m.Finished && m.StartTime <= currentTime)
            .Include(m => m.Markets)
                .ThenInclude(mk => mk.Selections)
            .ToList();
    }

    public void UpdateMatch(Match match)
    {
        _context.Entry(match).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeleteMatch(Match matchDelete)
    {
        var match = GetMatchById(matchDelete.Id);
        if (match != null)
            _context.Matches.Remove(match);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}