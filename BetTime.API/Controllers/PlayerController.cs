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

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null) return NotFound($"Player with ID {id} not found.");
            return Ok(player);
        }

        
        [HttpGet("team/{teamId}")]
        public IActionResult GetPlayersByTeam(int teamId)
        {
            var players = _playerService.GetPlayersByTeam(teamId);
            return Ok(players);
        }

      
        [HttpPost]
        public IActionResult CreatePlayer([FromBody] PlayerCreateDTO playerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var player = _playerService.CreatePlayer(playerDto);
            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, [FromBody] PlayerUpdateDTO playerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updatedPlayer = _playerService.UpdatePlayer(id, playerDto);
                return Ok(updatedPlayer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            try
            {
                _playerService.DeletePlayer(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}