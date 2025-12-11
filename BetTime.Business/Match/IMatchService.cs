using BetTime.Models;

public interface IMatchService
{
    Match CreateMatch(MatchCreateDTO matchCreateDTO);
    IEnumerable<Match> GetAllMatches();
    Match GetMatchById(int matchId);
    IEnumerable<Match> GetMatchesByLeague(int leagueId);
    IEnumerable<Match> GetMatchesByTeam(int teamId);
    void UpdateMatch(int matchId, MatchUpdateDTO dto);
    void DeleteMatch(int matchId);
}