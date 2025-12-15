using BetTime.Models;
using BetTime.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetTime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerMatchStatsController : ControllerBase
    {
        private readonly IPlayerMatchStatsService _playerMatchStatsService;

        public PlayerMatchStatsController(IPlayerMatchStatsService playerMatchStatsService)
        {
            _playerMatchStatsService = playerMatchStatsService;
        }

       
        [HttpGet("player/{playerId}/match/{matchId}")]
        public IActionResult GetPlayerMatchStats(int playerId, int matchId)
        {
            var stats = _playerMatchStatsService.GetPlayerMatchStats(playerId, matchId);
            if (stats == null)
                return NotFound($"Stats not found for player {playerId} in match {matchId}");

            return Ok(stats);
        }

        
        [HttpGet("match/{matchId}")]
        public IActionResult GetStatsByMatch(int matchId)
        {
            var stats = _playerMatchStatsService.GetPlayerMatchStatsByMatch(matchId);
            return Ok(stats);
        }

        
        [HttpPost]
        public IActionResult CreatePlayerMatchStats([FromBody] PlayerMatchStatsDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stats = _playerMatchStatsService.CreatePlayerMatchStats(dto);
            return Ok(stats);
        }

      
        [HttpPut("player/{playerId}/match/{matchId}")]
        public IActionResult UpdatePlayerMatchStats(
            int playerId,
            int matchId,
            [FromBody] PlayerMatchStatsUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedStats = _playerMatchStatsService.UpdatePlayerMatchStats(playerId, matchId, dto);
            if (updatedStats == null)
                return NotFound($"Stats not found for player {playerId} in match {matchId}");

            return Ok(updatedStats);
        }
    }
}