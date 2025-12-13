using Microsoft.AspNetCore.Mvc;
using BetTime.Business;
using BetTime.Models;
using Microsoft.AspNetCore.Authorization;

namespace BetTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketController : ControllerBase
    {
        private readonly ILogger<MarketController> _logger;
        private readonly IMarketService _marketService;

        public MarketController(ILogger<MarketController> logger, IMarketService marketService)
        {
            _logger = logger;
            _marketService = marketService;
        }

      
        [HttpGet("{marketId}")]
        public IActionResult GetMarketById(int marketId)
        {
            try
            {
                var market = _marketService.GetMarketById(marketId);
                return Ok(market);
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

      
        [HttpGet("match/{matchId}")]
        public IActionResult GetMarketsByMatch(int matchId)
        {
            try
            {
                var markets = _marketService.GetMarketsByMatch(matchId);
                return Ok(markets);
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
        [HttpPost("match/{matchId}")]
        public IActionResult CreateMarket(int matchId, [FromBody] MarketCreateDTO marketDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var market = _marketService.CreateMarket(matchId, marketDTO);
                return CreatedAtAction(nameof(GetMarketById), new { marketId = market.Id }, market);
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

}