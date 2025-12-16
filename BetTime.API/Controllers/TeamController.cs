using Microsoft.AspNetCore.Mvc;
using BetTime.Business;
using BetTime.Models;
using Microsoft.AspNetCore.Authorization;

namespace BetTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly ITeamService _teamService;

    public TeamController(ILogger<TeamController> logger, ITeamService teamService)
    {
        _logger = logger;
        _teamService = teamService;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateTeam([FromBody] TeamCreateDTO teamCreateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var team = _teamService.CreateTeam(teamCreateDTO);
            return CreatedAtRoute("GetTeamById", new { teamId = team.Id }, team);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning(knfex.Message);
            return NotFound(knfex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating team: {ex.Message}");
            return BadRequest($"Error creating team: {ex.Message}");
        }
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Team>> GetAllTeams()
    {
        try
        {
            var teams = _teamService.GetAllTeams();
            return Ok(teams);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching teams: {ex.Message}");
            return BadRequest($"Error fetching teams: {ex.Message}");
        }
    }

  
    [HttpGet("{teamId}", Name = "GetTeamById")]
    public IActionResult GetTeamById(int teamId)
    {
        try
        {
            var team = _teamService.GetTeamById(teamId);
            return Ok(team);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning(knfex.Message);
            return NotFound(knfex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

 
    [HttpGet("byLeague/{leagueId}", Name = "GetTeamsByLeague")]
    public IActionResult GetTeamsByLeague(int leagueId)
    {
        try
        {
            var teams = _teamService.GetTeamsByLeague(leagueId);
            return Ok(teams);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

  
    

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{teamId}")]
    public IActionResult UpdateTeam(int teamId, [FromBody] TeamUpdateDTO teamUpdateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            _teamService.UpdateTeam(teamId, teamUpdateDTO);
            return Ok($"Team with ID {teamId} updated successfully.");
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning(knfex.Message);
            return NotFound(knfex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

   
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{teamId}")]
    public IActionResult DeleteTeam(int teamId)
    {
        try
        {
            _teamService.DeleteTeam(teamId);
            return Ok($"Team with ID {teamId} deleted successfully.");
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning(knfex.Message);
            return NotFound(knfex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}