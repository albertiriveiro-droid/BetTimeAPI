using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;

public class TeamCreateDTO
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public int LeagueId { get; set; }
}
