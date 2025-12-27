using BetTime.Models;

public interface IMatchService
{
    Match CreateMatch(MatchCreateDTO matchCreateDTO);
    IEnumerable<MatchOutputDTO> GetAllMatches();
    MatchOutputDTO GetMatchById(int matchId);
    IEnumerable<MatchOutputDTO> GetMatchesByLeague(int leagueId);
    IEnumerable<MatchOutputDTO> GetMatchesByTeam(int teamId);
    void UpdateMatch(int matchId, MatchUpdateDTO dto);
    void DeleteMatch(int matchId);
}