using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;

public class Sport
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public ICollection<League> Leagues { get; set; }

    public Sport(){}

    public Sport(string name)
    {
        Name = name;
        Leagues = new List<League>();
    }
}