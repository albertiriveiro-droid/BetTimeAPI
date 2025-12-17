using BetTime.Data;
using BetTime.Models;
using BetTime.Services;

namespace BetTime.Business;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _repository;
    private readonly ILeagueRepository _leagueRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMarketService _marketService;
    private readonly IPlayerMatchStatsService _playerMatchStatsService;
    private readonly IPlayerMarketService _playerMarketService;

    public MatchService(
        IMatchRepository matchRepository,
        ILeagueRepository leagueRepository,
        ITeamRepository teamRepository,
        IPlayerRepository playerRepository,
         IMarketService marketService,
        IPlayerMatchStatsService playerMatchStatsService,
        IPlayerMarketService playerMarketService)
    {
        _repository = matchRepository;
        _leagueRepository = leagueRepository;
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _playerMatchStatsService = playerMatchStatsService;
        _playerMarketService = playerMarketService;
        _marketService= marketService;
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

      
        var homePlayers = _playerRepository.GetPlayerByTeam(match.HomeTeamId)
                                           .Where(p => p.IsActive).ToList();
        var awayPlayers = _playerRepository.GetPlayerByTeam(match.AwayTeamId)
                                           .Where(p => p.IsActive).ToList();
              if (!homePlayers.Any())
        throw new InvalidOperationException($"El equipo local (ID {match.HomeTeamId}) no tiene jugadores activos.");

        if (!awayPlayers.Any())
        throw new InvalidOperationException($"El equipo visitante (ID {match.AwayTeamId}) no tiene jugadores activos.");

      

        foreach (var player in homePlayers.Concat(awayPlayers))
        {
            
            _playerMatchStatsService.CreatePlayerMatchStats(new PlayerMatchStatsDTO
            {
                PlayerId = player.Id,
                MatchId = match.Id,
                Goals = 0,
                Assists = 0,
                YellowCards=0,
                RedCards=0,
                MinutesPlayed = 0
            });

           
            
        }
    var matchMarkets = new List<MarketCreateDTO>
{
    new MarketCreateDTO { MarketType = MarketType.OneXTwo, Description = "Resultado final" },
    new MarketCreateDTO { MarketType = MarketType.OverUnderGoals, Description = "Más/Menos de 2.5 goles" },
    new MarketCreateDTO { MarketType = MarketType.TotalCorners, Description = "Más/Menos de córners" },
    new MarketCreateDTO { MarketType = MarketType.BothToScore, Description = "Ambos equipos marcan" }
};

    foreach (var dto in matchMarkets)
    {
    _marketService.CreateMarket(match.Id, dto); 
    }

   
    var playerMarketTypes = new[]
{
    PlayerMarketType.Goal,
    PlayerMarketType.Assist,
    PlayerMarketType.YellowCard,
    PlayerMarketType.RedCard
    };

    foreach (var player in homePlayers.Concat(awayPlayers))
    {
    foreach (var type in playerMarketTypes)
    {
        _playerMarketService.CreatePlayerMarket(match.Id, new PlayerMarketCreateDTO
        {
            PlayerId = player.Id,
            PlayerMarketType = type
        });
    }
}
        return match;
    }

    public IEnumerable<Match> GetAllMatches() => _repository.GetAllMatches();

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