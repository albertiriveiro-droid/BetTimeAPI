using BetTime.Models;
namespace BetTime.Data;



public interface ITeamRepository
{
void AddTeam(Team team);
IEnumerable<Team> GetAllTeams();
Team GetTeamById(int TeamId);
IEnumerable<Team> GetTeamsByLeague( int LeagueId);
void DeleteTeam(Team team);
void UpdateTeam (Team team);
void SaveChanges();   
}