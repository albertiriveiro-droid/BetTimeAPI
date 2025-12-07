using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;


public class SportCreateDTO
{
    [Required]
    public string? Name { get; set; }
}