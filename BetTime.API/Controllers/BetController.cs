using Microsoft.AspNetCore.Mvc;
using BetTime.Business;
using BetTime.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BetTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BetController : ControllerBase
{
    private readonly ILogger<BetController> _logger;
    private readonly IBetService _betService;
    private readonly IAuthService _authService;

    public BetController(ILogger<BetController> logger, IBetService betService, IAuthService authService)
    {
        _logger = logger;
        _betService = betService;
        _authService = authService;
    }

    
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public IActionResult GetAllBets()
    {
        try
        {
            var bets = _betService.GetAllBets();
            return Ok(bets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{betId}")]
    public IActionResult GetBetById(int betId)
    {
        try
        {
            var bet = _betService.GetBetById(betId);

           
            if (!_authService.HasAccessToResource(bet.UserId, HttpContext.User))
                return Forbid();

            return Ok(bet);
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

   
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("user/{userId}")]
    public IActionResult GetBetsByUser(int userId)
    {
        if (!_authService.HasAccessToResource(userId, HttpContext.User))
            return Forbid();

        try
        {
            var bets = _betService.GetBetsByUser(userId);
            return Ok(bets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

  
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost]
    public IActionResult CreateBet([FromBody] BetCreateDTO betCreateDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
           
            if (!_authService.HasAccessToResource(betCreateDTO.UserId, HttpContext.User))
                return Forbid();

            var bet = _betService.CreateBet(betCreateDTO);
            return CreatedAtAction(nameof(GetBetById), new { betId = bet.Id }, bet);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning(knfex.Message);
            return NotFound(knfex.Message);
        }
        catch (ArgumentException aex)
        {
            _logger.LogWarning(aex.Message);
            return BadRequest(aex.Message);
        }
        catch (InvalidOperationException ioex)
        {
            _logger.LogWarning(ioex.Message);
            return BadRequest(ioex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

[Authorize(Roles = Roles.Admin)]
[HttpGet("match/{matchId}")]
public IActionResult GetBetsByMatch(int matchId)
{
    try
    {
        var bets = _betService.GetBetsByMatch(matchId);
        return Ok(bets);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
    }
}



    [Authorize(Roles = Roles.Admin)]
    [HttpPut("resolve/{betId}")]
    public IActionResult ResolveBet(int betId)
    {
        try
        {
            var bet = _betService.ResolveBet(betId);
            return Ok(bet);
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }


[Authorize(Roles = Roles.Admin)]
[HttpPut("resolve/match/{matchId}")]
public IActionResult ResolveBetsForMatch(int matchId)
{
    try
    {
        _betService.ResolveBetsForMatch(matchId);
        return Ok($"All bets for match {matchId} resolved successfully.");
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
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
    }
}
   
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("won/{userId}")]
    public IActionResult GetWonBets(int userId)
    {
        if (!_authService.HasAccessToResource(userId, HttpContext.User))
            return Forbid();

        try
        {
            var bets = _betService.GetWonBets(userId);
            return Ok(bets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("lost/{userId}")]
    public IActionResult GetLostBets(int userId)
    {
        if (!_authService.HasAccessToResource(userId, HttpContext.User))
            return Forbid();

        try
        {
            var bets = _betService.GetLostBets(userId);
            return Ok(bets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }


[Authorize(Roles = Roles.Admin + "," + Roles.User)]
[HttpGet("active/{userId}")]
public IActionResult GetActiveBets(int userId)
{
    if (!_authService.HasAccessToResource(userId, HttpContext.User)) return Forbid();

    try
    {
        var bets = _betService.GetBetsByUser(userId).Where(b => !b.Won.HasValue);
        return Ok(bets);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
    }
}


[Authorize(Roles = Roles.Admin + "," + Roles.User)]
[HttpGet("finished/{userId}")]
public IActionResult GetFinishedBets(int userId)
{
    if (!_authService.HasAccessToResource(userId, HttpContext.User)) return Forbid();

    try
    {
        var bets = _betService.GetBetsByUser(userId).Where(b => b.Won.HasValue);
        return Ok(bets);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
    }
}
}