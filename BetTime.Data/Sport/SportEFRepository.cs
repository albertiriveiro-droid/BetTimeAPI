using BetTime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BetTime.Data;

public class SportEFRepository : ISportRepository
{
    
private readonly BetTimeContext _context;

public SportEFRepository(BetTimeContext context)
    {
    _context=context;
    }

public void AddSport(Sport sport)
    {
     _context.Sports.Add(sport);  
    SaveChanges();
    }

public IEnumerable<Sport> GetAllSports()
{
    return _context.Sports
        .Include(s => s.Leagues) 
        .ToList();
}
public Sport GetSportById(int sportId)
{
    var sport = _context.Sports
        .Include(s => s.Leagues) 
        .FirstOrDefault(s => s.Id == sportId);

    return sport;
}

public void DeleteSport(Sport sportDelete)
    {
    var sport= GetSportById(sportDelete.Id);
    _context.Sports.Remove(sport);
    SaveChanges();
    }

public void UpdateSport(Sport sport)
    {
        _context.Entry(sport).State = EntityState.Modified;
            SaveChanges();
    }

public bool SportNameExists(string name)
{
    return _context.Sports.Any(s => s.Name == name);
}


public void SaveChanges()
    {
    _context.SaveChanges();    
    }
}