using System.ComponentModel.DataAnnotations;
namespace BetTime.Models;

public class LeagueCreateDTO
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public int SportId { get; set; }
}