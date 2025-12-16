using BetTime.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace BetTime.Data;


public interface ISportRepository
{

 void AddSport(Sport sport);
 IEnumerable<Sport> GetAllSports();
 Sport GetSportById(int SportId);
 void DeleteSport(Sport sport);
 void UpdateSport(Sport sport);
 bool SportNameExists(string name);
 void SaveChanges();  
}

