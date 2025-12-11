using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;
    public class MatchCreateDTO
    {
        [Required]
        public int LeagueId { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int DurationMinutes { get; set; } = 90;

    }
