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
    var teams= _context.Teams;
    return teams;
    }
public IEnumerable<Team> GetTeamsByLeague(int leagueId)
{
    return _context.Teams
        .Where(t => t.LeagueId == leagueId)
        .Include(t => t.League)
        .ToList();
}

public Team GetTeamById(int TeamId)
    {
         var team = _context.Teams.FirstOrDefault(team => team.Id == TeamId);
        return team;  
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


public void SaveChanges()
    {
    _context.SaveChanges();    
    }
}