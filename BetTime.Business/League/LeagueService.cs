using BetTime.Data;
using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Business;

public class LeagueService : ILeagueService
{
    private readonly ILeagueRepository _repository;
    private readonly ISportRepository _sportRepository;

    public LeagueService(ILeagueRepository repository, ISportRepository sportRepository)
    {
        _repository = repository;
        _sportRepository = sportRepository;
    }


public League CreateLeague(LeagueCreateDTO leagueCreateDTO)
{
    if (string.IsNullOrWhiteSpace(leagueCreateDTO.Name))
        throw new ArgumentException("League name is required");

  
    var sport = _sportRepository.GetSportById(leagueCreateDTO.SportId);
    if (sport == null)
        throw new KeyNotFoundException($"Sport with ID {leagueCreateDTO.SportId} not found");

  
    if (_repository.LeagueNameExists(leagueCreateDTO.Name))
        throw new InvalidOperationException("A league with this name already exists.");

  
    var league = new League(leagueCreateDTO.Name.Trim(), leagueCreateDTO.SportId);

    try
    {
        _repository.AddLeague(league);
    }
    catch (DbUpdateException)
    {
        throw new InvalidOperationException("A league with this name already exists.");
    }

    return league;
}


public IEnumerable<League> GetAllLeagues()
    {
   return _repository.GetAllLeagues();
    }


public League GetLeagueById(int leagueId)
    {

var league= _repository.GetLeagueById(leagueId);
        if (league == null)
        {
         throw new KeyNotFoundException($"League with ID {leagueId} not found");  
        }
        return league;
    }

public void DeleteLeague(int leagueId)
    {
    
var league= _repository.GetLeagueById(leagueId);
        if (league == null)
        {
      throw new KeyNotFoundException($"League with ID {leagueId} not found");  
        }
_repository.DeleteLeague(league);
    }

 public void UpdateLeague(int leagueId, LeagueUpdateDTO dto)
{
    var league = _repository.GetLeagueById(leagueId)
        ?? throw new KeyNotFoundException($"League with ID {leagueId} not found");

    if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != league.Name)
    {
        if (_repository.LeagueNameExists(dto.Name))
            throw new InvalidOperationException("A league with this name already exists.");

        league.Name = dto.Name.Trim();
    }

    if (dto.SportId.HasValue && dto.SportId.Value != league.SportId)
    {
        var sport = _sportRepository.GetSportById(dto.SportId.Value);
        if (sport == null)
            throw new KeyNotFoundException($"Sport with ID {dto.SportId.Value} not found");

        league.SportId = dto.SportId.Value;
    }

 
    try
    {
        _repository.UpdateLeague(league);
    }
    catch (DbUpdateException)
    {
        throw new InvalidOperationException("A league with this name already exists.");
    }
}

public IEnumerable<League> GetLeaguesBySport(int sportId)
    {
     return _repository.GetLeaguesBySport(sportId);
      
    }

}