using BetTime.Models;
namespace BetTime.Data
{
    public interface IMatchRepository
    {
        void AddMatch(Match match);
        IEnumerable<Match> GetAllMatches();
        Match? GetMatchById(int matchId);
        IEnumerable<Match> GetMatchesByLeague(int leagueId);
        IEnumerable<Match> GetMatchesByTeam(int teamId);
        IEnumerable<Match> GetUpcomingMatches();
        IEnumerable<Match> GetFinishedMatches();
        void UpdateMatch(Match match);
        void DeleteMatch(Match match);
        void SaveChanges();
    }
}