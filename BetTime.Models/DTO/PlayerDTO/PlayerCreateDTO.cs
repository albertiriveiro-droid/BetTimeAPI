using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;

public class PlayerCreateDTO
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public int TeamId { get; set; }

    [Required]
    public int ShirtNumber { get; set; }

    [Required]
    public PlayerPosition Position { get; set; }
}