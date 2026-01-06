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
    private readonly IMarketGeneratorService _marketGenerator;
    private readonly IPlayerMarketGeneratorService _playerMarketGenerator;
    

    public MatchService(
        IMatchRepository matchRepository,
        ILeagueRepository leagueRepository,
        ITeamRepository teamRepository,
        IPlayerRepository playerRepository,
         IMarketService marketService,
        IPlayerMatchStatsService playerMatchStatsService,
        IPlayerMarketService playerMarketService,
        IMarketGeneratorService marketGenerator,
        IPlayerMarketGeneratorService playerMarketGenerator)
    {
        _repository = matchRepository;
        _leagueRepository = leagueRepository;
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _playerMatchStatsService = playerMatchStatsService;
        _playerMarketService = playerMarketService;
        _marketService= marketService;
        _marketGenerator = marketGenerator;
        _playerMarketGenerator = playerMarketGenerator;
    }

   public Match CreateMatch(MatchCreateDTO matchCreateDTO)
{
    try
    {
        
        if (matchCreateDTO.StartTime <= DateTime.UtcNow)
            throw new InvalidOperationException("No se pueden crear partidos en el pasado.");

        if (matchCreateDTO.HomeTeamId == matchCreateDTO.AwayTeamId)
            throw new ArgumentException("Un partido no puede tener el mismo equipo como local y visitante.");

       
        var league = _leagueRepository.GetLeagueById(matchCreateDTO.LeagueId);
        if (league == null)
            throw new KeyNotFoundException($"Liga con ID {matchCreateDTO.LeagueId} no encontrada.");

        
        var homeTeam = _teamRepository.GetTeamById(matchCreateDTO.HomeTeamId);
        if (homeTeam == null)
            throw new KeyNotFoundException($"Equipo local con ID {matchCreateDTO.HomeTeamId} no encontrado.");

        var awayTeam = _teamRepository.GetTeamById(matchCreateDTO.AwayTeamId);
        if (awayTeam == null)
            throw new KeyNotFoundException($"Equipo visitante con ID {matchCreateDTO.AwayTeamId} no encontrado.");

       
        if (homeTeam.LeagueId != matchCreateDTO.LeagueId)
            throw new InvalidOperationException($"El equipo local (ID {homeTeam.Id}) no pertenece a la liga seleccionada (ID {matchCreateDTO.LeagueId}).");

        if (awayTeam.LeagueId != matchCreateDTO.LeagueId)
            throw new InvalidOperationException($"El equipo visitante (ID {awayTeam.Id}) no pertenece a la liga seleccionada (ID {matchCreateDTO.LeagueId}).");

        
        var match = new Match(
            matchCreateDTO.LeagueId,
            matchCreateDTO.HomeTeamId,
            matchCreateDTO.AwayTeamId,
            matchCreateDTO.StartTime,
            matchCreateDTO.DurationMinutes
        );

        _repository.AddMatch(match);
        Console.WriteLine($"Match created with ID {match.Id}");

        
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
                YellowCards = 0,
                RedCards = 0,
                MinutesPlayed = 0
            });
        }
        Console.WriteLine("PlayerMatchStats created for all players");

      
        _marketGenerator.GenerateMarketsForMatch(match.Id);
        Console.WriteLine("Markets generated for match");

        foreach (var player in homePlayers.Concat(awayPlayers))
        {
            _playerMarketGenerator.GenerateMarketsForPlayer(match.Id, player.Id);
        }
        Console.WriteLine("PlayerMarkets generated for all players");

        return match;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error creating match: " + ex.ToString());
        throw; 
    }
}
    public IEnumerable<MatchOutputDTO> GetAllMatches() =>
    _repository.GetAllMatches().Select(MapToDTO);

    public MatchOutputDTO GetMatchById(int matchId)
    {
    var match = _repository.GetMatchById(matchId);
    if (match == null)
        throw new KeyNotFoundException($"Match with ID {matchId} not found");

    return MapToDTO(match);
}

    public IEnumerable<MatchOutputDTO> GetMatchesByLeague(int leagueId) =>
    _repository.GetMatchesByLeague(leagueId).Select(MapToDTO);


    public IEnumerable<MatchOutputDTO> GetMatchesByTeam(int teamId) =>
    _repository.GetMatchesByTeam(teamId).Select(MapToDTO);

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

    private MatchOutputDTO MapToDTO(Match match)
{
    return new MatchOutputDTO
    {
        Id = match.Id,
        LeagueId = match.LeagueId,
        LeagueName = match.League?.Name ?? "",
        HomeTeamId = match.HomeTeamId,
        HomeTeamName = match.HomeTeam?.Name ?? "",
        AwayTeamId = match.AwayTeamId,
        AwayTeamName = match.AwayTeam?.Name ?? "",
        StartTime = match.StartTime,
        HomeScore = match.HomeScore,
        AwayScore = match.AwayScore,
        HomeCorners = match.HomeCorners,
        AwayCorners = match.AwayCorners,
        DurationMinutes = match.DurationMinutes,
        Finished = match.Finished,
        PlayerStats = match.PlayerMatchStats.Select(p => new PlayerMatchStatsDTO
        {
            PlayerId = p.PlayerId,
            MatchId = p.MatchId,
            Goals = p.Goals,
            Assists = p.Assists,
            YellowCards = p.YellowCard,   
            RedCards = p.RedCard,         
            MinutesPlayed = p.MinutesPlayed
        }).ToList()

    };
}

}