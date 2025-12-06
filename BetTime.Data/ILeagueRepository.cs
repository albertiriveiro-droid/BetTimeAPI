using BetTime.Models;

namespace BetTime.Data;


public interface ILeagueRepository
{
   void AddLeague(League league);
    IEnumerable<League> GetAllLeagues();
    League GetLeagueById(int LeagueId);
    IEnumerable<League> GetLeaguesBySport(int SportId);
    void DeleteLeague(League league);
    void UpdateLeague( League league);
    void SaveChanges();
}