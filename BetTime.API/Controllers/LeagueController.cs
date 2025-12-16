using Microsoft.AspNetCore.Mvc;
using BetTime.Business;
using BetTime.Models;
using Microsoft.AspNetCore.Authorization;

namespace BetTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeagueController : ControllerBase
{
    private readonly ILogger<LeagueController> _logger;
    private readonly ILeagueService _leagueService;

    public LeagueController(ILogger<LeagueController> logger, ILeagueService leagueService)
    {
        _logger = logger;
        _leagueService = leagueService;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateLeague([FromBody] LeagueCreateDTO leagueCreateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var league = _leagueService.CreateLeague(leagueCreateDTO);
            return CreatedAtRoute("GetLeagueById", new { leagueId = league.Id }, league);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning(knfex.Message);
            return NotFound(knfex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating league: {ex.Message}");
            return BadRequest($"Error creating league: {ex.Message}");
        }
    }

   
    [HttpGet]
    public ActionResult<IEnumerable<League>> GetAllLeagues()
    {
        try
        {
            var leagues = _leagueService.GetAllLeagues();
            return Ok(leagues);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching leagues: {ex.Message}");
            return BadRequest($"An error occurred while fetching leagues: {ex.Message}");
        }
    }

  
    [HttpGet("{leagueId}", Name = "GetLeagueById")]
    public IActionResult GetLeagueById(int leagueId)
    {
        try
        {
            var league = _leagueService.GetLeagueById(leagueId);
            return Ok(league);
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

 
    [HttpGet("bySport/{sportId}", Name = "GetLeaguesBySport")]
    public IActionResult GetLeaguesBySport(int sportId)
    {
        try
        {
            var leagues = _leagueService.GetLeaguesBySport(sportId);
            return Ok(leagues);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

   
    

    
    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{leagueId}")]
    public IActionResult UpdateLeague(int leagueId, [FromBody] LeagueUpdateDTO leagueUpdateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            _leagueService.UpdateLeague(leagueId, leagueUpdateDTO);
            return Ok($"League with ID {leagueId} updated successfully.");
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
    [HttpDelete("{leagueId}")]
    public IActionResult DeleteLeague(int leagueId)
    {
        try
        {
            _leagueService.DeleteLeague(leagueId);
            return Ok($"League with ID {leagueId} deleted successfully.");
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