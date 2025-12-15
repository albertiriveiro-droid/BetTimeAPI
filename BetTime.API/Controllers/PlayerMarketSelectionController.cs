using BetTime.Services;
using BetTime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class PlayerMarketSelectionController : ControllerBase
{
    private readonly IPlayerMarketSelectionService _service;

    public PlayerMarketSelectionController(IPlayerMarketSelectionService service)
    {
        _service = service;
    }

   
    [HttpPost("matchplayermarket/{playerMarketId}/selection")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult CreateSelection(int playerMarketId, [FromBody] PlayerMarketSelectionCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var selection = _service.CreatePlayerSelection(playerMarketId, dto);
            return CreatedAtAction(nameof(GetSelectionById), new { id = selection.Id }, selection);
        }
        catch (KeyNotFoundException knfEx)
        {
            return NotFound(knfEx.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetSelectionById(int id)
    {
        try
        {
            var selection = _service.GetSelectionByPlayerId(id);
            return Ok(selection);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("bymarket/{playerMarketId}")]
    public IActionResult GetSelectionsByPlayerMarket(int playerMarketId)
    {
        try
        {
            var selections = _service.GetSelectionsByPlayerMarket(playerMarketId);
            return Ok(selections);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}