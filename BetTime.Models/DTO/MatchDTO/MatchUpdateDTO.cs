namespace BetTime.Models
{
    public class MatchUpdateDTO
    {
        public DateTime? StartTime { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public bool? Finished { get; set; }

        // NEW → Live simulation controls
        public int? DurationMinutes { get; set; }

    }
}