using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BetTime.Models;

public class League
{   
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [ForeignKey("Sport")]
    public int SportId { get; set; }
    [JsonIgnore]
    public Sport? Sport { get; set; }

    public ICollection<Team> Teams { get; set; }
    public ICollection<Match> Matches { get; set; }

  
    public League(){}

    public League(string name, int sportId)
    {
        Name = name;
        SportId = sportId;
        Teams = new List<Team>();
        Matches = new List<Match>();
    }
}