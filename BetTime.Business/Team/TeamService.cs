using BetTime.Data;
using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Business;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _repository;
    private readonly ILeagueRepository _leagueRepository;

    public TeamService(ITeamRepository repository, ILeagueRepository leagueRepository)
    {
        _repository = repository;
        _leagueRepository = leagueRepository;
    }

public Team CreateTeam(TeamCreateDTO teamCreateDTO)
{
    
    if (string.IsNullOrWhiteSpace(teamCreateDTO.Name))
        throw new ArgumentException("Team name is required");

    
    var league = _leagueRepository.GetLeagueById(teamCreateDTO.LeagueId);
    if (league == null)
        throw new KeyNotFoundException($"League with ID {teamCreateDTO.LeagueId} not found");

    if (_repository.TeamNameExists(teamCreateDTO.Name))
        throw new InvalidOperationException("A team with this name already exists.");

  
    var team = new Team(teamCreateDTO.Name.Trim(), teamCreateDTO.LeagueId);

    try
    {
        _repository.AddTeam(team);
    }
    catch (DbUpdateException)
    {

        throw new InvalidOperationException("A team with this name already exists.");
    }

    return team;
}

public IEnumerable<Team> GetAllTeams()
    {

    return _repository.GetAllTeams();
    }

public IEnumerable<Team> GetTeamsByLeague(int leagueId)
    {
return _repository.GetTeamsByLeague(leagueId);
    }


public Team GetTeamById(int teamId)
    {
 var team= _repository.GetTeamById(teamId);
        if (team == null)
        {
            throw new KeyNotFoundException($"Team with ID {teamId} not found");
        } 
    return team;      
    }
public void DeleteTeam(int teamId)
    {
        var team = _repository.GetTeamById(teamId);
        if (team == null)
            throw new KeyNotFoundException($"Team with ID {teamId} not found");

        _repository.DeleteTeam(team);
    
}

public void UpdateTeam(int id, TeamUpdateDTO dto)
{
    var team = _repository.GetTeamById(id)
        ?? throw new KeyNotFoundException($"Team with ID {id} not found");

    if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != team.Name)
    {
        if (_repository.TeamNameExists(dto.Name))
            throw new InvalidOperationException("A team with this name already exists.");

        team.Name = dto.Name.Trim();
    }

    
    if (dto.LeagueId.HasValue && dto.LeagueId.Value != team.LeagueId)
    {
        var league = _leagueRepository.GetLeagueById(dto.LeagueId.Value);
        if (league == null)
            throw new KeyNotFoundException($"League with ID {dto.LeagueId.Value} not found");

        team.LeagueId = dto.LeagueId.Value;
    }

    try
    {
        _repository.UpdateTeam(team);
    }
    catch (DbUpdateException)
    {
        
        throw new InvalidOperationException("A team with this name already exists.");
    }
}

}