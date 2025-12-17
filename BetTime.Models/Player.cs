using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BetTime.Models;

public class Player
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public PlayerPosition Position { get; set; } 

    [Required]
    public int TeamId { get; set; }

    [JsonIgnore]
    public Team? Team { get; set; }

    public int ShirtNumber { get; set; }

    public bool IsActive { get; set; } = true;

    [JsonIgnore]
        public int TempGoals { get; set; }
        [JsonIgnore]
        
        public int TempAssists { get; set; }
        [JsonIgnore]
        public int TempMinutesPlayed { get; set; }

        [JsonIgnore]
         public int TempYellowCards { get; set; }
        [JsonIgnore] 
        public int TempRedCards { get; set; }

    public ICollection<PlayerMatchStats> MatchStats { get; set; } = new List<PlayerMatchStats>();

    public ICollection<PlayerMarket> PlayerMarkets { get; set; } = new List<PlayerMarket>();

    // Relación con PlayerMatchStats

    public Player() { }

    public Player(string name, int teamId, int shirtNumber, PlayerPosition position)
    {
        Name = name;
        TeamId = teamId;
        ShirtNumber = shirtNumber;
        Position= position;
        MatchStats = new List<PlayerMatchStats>();
    }
}