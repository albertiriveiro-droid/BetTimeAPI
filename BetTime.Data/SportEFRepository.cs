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
    var sports= _context.Sports;
    return sports;
    }
public Sport GetSportById(int SportId)
    {
         var sport = _context.Sports.FirstOrDefault(sport => sport.Id == SportId);
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


public void SaveChanges()
    {
    _context.SaveChanges();    
    }
}