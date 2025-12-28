using BetTime.Business;
using BetTime.Models;
using BetTime.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PlayerMarketController : ControllerBase
{
    private readonly IPlayerMarketService _service;
    private readonly ILogger<PlayerMarketController> _logger;

    public PlayerMarketController(IPlayerMarketService service, ILogger<PlayerMarketController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("match/{matchId}/playermarket")]
    public IActionResult CreatePlayerMarket(int matchId, [FromBody] PlayerMarketCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var market = _service.CreatePlayerMarket(matchId, dto);
            return CreatedAtAction(nameof(GetPlayerMarketById), new { id = market.Id }, market);
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

    [HttpGet("{id}")]
    public IActionResult GetPlayerMarketById(int id)
    {
        try
        {
            var market = _service.GetPlayerMarketById(id);
            return Ok(market);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("bymatch/{matchId}")]
    public IActionResult GetPlayerMarketsByMatch(int matchId)
    {
        try
        {
            var markets = _service.GetPlayerMarketsByMatch(matchId);
            return Ok(markets);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("bymatch/details/{matchId}")]
    public IActionResult GetPlayerMarketsByMatchDetails(int matchId)
    {
    var markets = _service.GetPlayerMarketsWithPlayerByMatch(matchId);
    return Ok(markets);
}
}