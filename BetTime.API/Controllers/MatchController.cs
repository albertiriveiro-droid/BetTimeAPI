using Microsoft.AspNetCore.Mvc;
using BetTime.Business;
using BetTime.Models;
using Microsoft.AspNetCore.Authorization;

namespace BetTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly ILogger<MatchController> _logger;
    private readonly IMatchService _matchService;

    public MatchController(ILogger<MatchController> logger, IMatchService matchService)
    {
        _logger = logger;
        _matchService = matchService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MatchOutputDTO>> GetAllMatches()
{
    try
    {
        var matches = _matchService.GetAllMatches();
        return Ok(matches);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
    }
}

    
    [HttpGet("{matchId}")]
    public ActionResult<MatchOutputDTO> GetMatchById(int matchId)
    {
    try
    {
        var match = _matchService.GetMatchById(matchId);
        return Ok(match);
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
    
[HttpGet("byLeague/{leagueId}", Name = "GetMatchesByLeague")]
public IActionResult GetMatchesByLeague(int leagueId)
{
    try
    {
        var matches = _matchService.GetMatchesByLeague(leagueId);
        return Ok(matches);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
    }
}


    
[HttpGet("byTeam/{teamId}", Name = "GetMatchesByTeam")]
public IActionResult GetMatchesByTeam(int teamId)
{
    try
    {
        var matches = _matchService.GetMatchesByTeam(teamId);
        return Ok(matches); 
    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
    }
}

[Authorize(Roles = Roles.Admin)]
[HttpPost]
public IActionResult CreateMatch([FromBody] MatchCreateDTO matchCreateDTO)
{
    if (!ModelState.IsValid) 
        return BadRequest(ModelState);

    try
    {
        var match = _matchService.CreateMatch(matchCreateDTO);

        
        return Ok(new 
        {
            Message = "Match created successfully",
            MatchId = match.Id,
            LeagueId = match.LeagueId,
            HomeTeamId = match.HomeTeamId,
            AwayTeamId = match.AwayTeamId,
            StartTime = match.StartTime,
            DurationMinutes = match.DurationMinutes
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error creating match");
        return StatusCode(500, "Internal server error creating match");
    }
}


    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{matchId}")]
    public IActionResult UpdateMatch(int matchId, [FromBody] MatchUpdateDTO matchUpdateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            _matchService.UpdateMatch(matchId, matchUpdateDTO);
            return Ok($"Match with ID {matchId} updated successfully.");
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning(knfex.Message);
            return NotFound(knfex.Message);
        }
        catch (InvalidOperationException ioex)
        {
            _logger.LogWarning(ioex.Message);
            return BadRequest(ioex.Message);
        }
        catch (ArgumentException aex)
        {
            _logger.LogWarning(aex.Message);
            return BadRequest(aex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{matchId}")]
    public IActionResult DeleteMatch(int matchId)
    {
        try
        {
            _matchService.DeleteMatch(matchId);
            return Ok($"Match with ID {matchId} deleted successfully.");
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