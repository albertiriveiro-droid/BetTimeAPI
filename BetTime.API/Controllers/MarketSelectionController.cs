using BetTime.Business;
using BetTime.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetTime.Api.Controllers;

[ApiController]
[Route("api")]
public class MarketSelectionController : ControllerBase
{
    private readonly IMarketSelectionService _selectionService;

    public MarketSelectionController(IMarketSelectionService selectionService)
    {
        _selectionService = selectionService;
    }

    [HttpPost("markets/{marketId}/selections")]
public IActionResult AddSelection(int marketId, [FromBody] MarketSelectionCreateDTO dto)
{
    try
    {
        var selection = _selectionService.CreateSelection(marketId, dto);
        return CreatedAtAction(nameof(GetSelectionById), new { selectionId = selection.Id }, selection);
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(new { message = ex.Message });
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new { message = ex.Message });
    }
}
    [HttpGet("markets/{marketId}/selections")]
public ActionResult<IEnumerable<MarketSelection>> GetSelectionsByMarket(int marketId)
{
    try
    {
        var selections = _selectionService.GetSelectionsByMarket(marketId);
        return Ok(selections);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new { message = ex.Message });
    }
}

[HttpGet("selections/{selectionId}")]
public ActionResult<MarketSelection> GetSelectionById(int selectionId)
{
    try
    {
        var selection = _selectionService.GetSelectionById(selectionId);
        return Ok(selection);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new { message = ex.Message });
    }
}
}