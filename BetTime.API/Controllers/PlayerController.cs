using BetTime.Models;
using BetTime.Business;
using Microsoft.AspNetCore.Mvc;
using BetTime.Services;

namespace BetTime.Controllers
{
   [ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }


    [HttpPost]
    public IActionResult CreatePlayer([FromBody] PlayerCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var player = _playerService.CreatePlayer(dto);

            return CreatedAtRoute(
                "GetPlayerById",
                new { playerId = player.Id },
                player
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
   
    [HttpGet("{playerId}", Name = "GetPlayerById")]
    public IActionResult GetPlayerById(int playerId)
    {
        var player = _playerService.GetPlayerById(playerId);
        if (player == null)
            return NotFound($"Player with ID {playerId} not found.");
        
        return Ok(player);
    }

    
  
    [HttpGet("team/{teamId}")]
    public IActionResult GetPlayersByTeam(int teamId)
    {
        var players = _playerService.GetPlayersByTeam(teamId);
        return Ok(players);
    }

   
    

   
    [HttpPut("{playerId}")]
    public IActionResult UpdatePlayer(int playerId, [FromBody] PlayerUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedPlayer = _playerService.UpdatePlayer(playerId, dto);
            return Ok(updatedPlayer);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    
    [HttpDelete("{playerId}")]
    public IActionResult DeletePlayer(int playerId)
    {
        try
        {
            _playerService.DeletePlayer(playerId);
           return Ok($"Player with ID {playerId} deleted successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

}