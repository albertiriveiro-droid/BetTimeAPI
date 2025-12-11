using BetTime.Data;
using BetTime.Models;

namespace BetTime.Business;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _repository;
    private readonly ILeagueRepository _leagueRepository;
    private readonly ITeamRepository _teamRepository;

    public MatchService(
        IMatchRepository matchRepository,
        ILeagueRepository leagueRepository,
        ITeamRepository teamRepository)
    {
        _repository = matchRepository;
        _leagueRepository = leagueRepository;
        _teamRepository = teamRepository;
    }

    public Match CreateMatch(MatchCreateDTO matchCreateDTO)
{
    if (matchCreateDTO.HomeTeamId == matchCreateDTO.AwayTeamId)
        throw new ArgumentException("A match cannot have the same team as home and away.");   
        
    if (_leagueRepository.GetLeagueById(matchCreateDTO.LeagueId) == null)
        throw new KeyNotFoundException($"League with ID {matchCreateDTO.LeagueId} not found");

    if (_teamRepository.GetTeamById(matchCreateDTO.HomeTeamId) == null)
        throw new KeyNotFoundException($"Home team with ID {matchCreateDTO.HomeTeamId} not found");

    if (_teamRepository.GetTeamById(matchCreateDTO.AwayTeamId) == null)
        throw new KeyNotFoundException($"Away team with ID {matchCreateDTO.AwayTeamId} not found");

    var match = new Match(
        matchCreateDTO.LeagueId,
        matchCreateDTO.HomeTeamId,
        matchCreateDTO.AwayTeamId,
        matchCreateDTO.StartTime,
        matchCreateDTO.DurationMinutes
    );

    _repository.AddMatch(match);
    return match;
}    public IEnumerable<Match> GetAllMatches() => _repository.GetAllMatches();

    public Match GetMatchById(int matchId)
    {
        var match = _repository.GetMatchById(matchId);
        if (match == null)
            throw new KeyNotFoundException($"Match with ID {matchId} not found");
        return match;
    }

    public IEnumerable<Match> GetMatchesByLeague(int leagueId) =>
        _repository.GetMatchesByLeague(leagueId);

    public IEnumerable<Match> GetMatchesByTeam(int teamId) =>
        _repository.GetMatchesByTeam(teamId);

    public void UpdateMatch(int matchId, MatchUpdateDTO dto)
    {
        var match = _repository.GetMatchById(matchId);
        if (match == null)
            throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Finished)
            throw new InvalidOperationException("Cannot update a finished match.");

        if (dto.StartTime.HasValue)
            match.StartTime = dto.StartTime.Value;

        if (dto.HomeScore.HasValue)
            match.HomeScore = dto.HomeScore.Value;

        if (dto.AwayScore.HasValue)
            match.AwayScore = dto.AwayScore.Value;

        if (dto.Finished.HasValue)
            match.Finished = dto.Finished.Value;

        if (dto.DurationMinutes.HasValue)
            match.DurationMinutes = dto.DurationMinutes.Value;

        _repository.UpdateMatch(match);
    }

    public void DeleteMatch(int matchId)
    {
        var match = _repository.GetMatchById(matchId);
        if (match == null)
            throw new KeyNotFoundException($"Match with ID {matchId} not found");

        _repository.DeleteMatch(match);
    }
}