using BetTime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BetTime.Data;

public class TeamEFRepository : ITeamRepository
{
    
private readonly BetTimeContext _context;

public TeamEFRepository(BetTimeContext context)
    {
    _context=context;
    }

public void AddTeam(Team team)
    {
     _context.Teams.Add(team);  
    SaveChanges();
    }

public IEnumerable<Team> GetAllTeams()
    {
     return _context.Teams
        .Include(t=>t.Players)
        .ToList();
    }
public IEnumerable<Team> GetTeamsByLeague(int leagueId)
{
    return _context.Teams
        .Where(t => t.LeagueId == leagueId)
        .Include(t => t.League)
        .Include(t => t.Players) 
        .Include(t=>t.HomeMatches)
        .Include(t=>t.AwayMatches)
        .ToList();
}

public Team? GetTeamById(int teamId)
{
    return _context.Teams
        .Include(t => t.Players) 
         .Include(t=>t.HomeMatches)
        .Include(t=>t.AwayMatches)
        .FirstOrDefault(t => t.Id == teamId);
}

public void DeleteTeam(Team teamDelete)
    {
    var team= GetTeamById(teamDelete.Id);
    _context.Teams.Remove(team);
    SaveChanges();
    }

public void UpdateTeam(Team team)
    {
        _context.Entry(team).State = EntityState.Modified;
            SaveChanges();
    }

public bool TeamNameExists(string name)
{
    return _context.Teams.Any(t => t.Name == name);
}



public void SaveChanges()
    {
    _context.SaveChanges();    
    }
}