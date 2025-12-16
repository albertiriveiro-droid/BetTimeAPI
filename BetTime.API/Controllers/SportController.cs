using Microsoft.AspNetCore.Mvc;
using BetTime.Business;
using BetTime.Models;
using Microsoft.AspNetCore.Authorization;

namespace BetTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SportController : ControllerBase
{
    private readonly ILogger<SportController> _logger;
    private readonly ISportService _sportService;

    public SportController(ILogger<SportController> logger, ISportService sportService)
    {
        _logger = logger;
        _sportService = sportService;
    }


    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateSport([FromBody] SportCreateDTO sportCreateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var sport = _sportService.CreateSport(sportCreateDTO);
            return CreatedAtRoute("GetSportById", new { sportId = sport.Id }, sport);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating sport: {ex.Message}");
            return BadRequest($"Error creating sport: {ex.Message}");
        }
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Sport>> GetAllSports()
    {
        try
        {
            var sports = _sportService.GetAllSports();
            return Ok(sports);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching sports: {ex.Message}");
            return BadRequest($"An error occurred while fetching sports: {ex.Message}");
        }
    }

    
    [HttpGet("{sportId}", Name = "GetSportById")]
    public IActionResult GetSportById(int sportId)
    {
        try
        {
            var sport = _sportService.GetSportById(sportId);
            return Ok(sport);
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
    [HttpPut("{sportId}")]
    public IActionResult UpdateSport(int sportId, [FromBody] SportUpdateDTO sportUpdateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            _sportService.UpdateSport(sportId, sportUpdateDTO);
            return Ok($"Sport with ID {sportId} updated successfully.");
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
    [HttpDelete("{sportId}")]
    public IActionResult DeleteSport(int sportId)
    {
        try
        {
            _sportService.DeleteSport(sportId);
            return Ok($"Sport with ID {sportId} deleted successfully.");
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