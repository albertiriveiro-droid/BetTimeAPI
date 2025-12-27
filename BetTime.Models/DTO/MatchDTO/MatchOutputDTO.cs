namespace BetTime.Models
{
    public class MatchOutputDTO
    {
       public int Id { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; } = null!;

        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; } = null!;

        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int HomeCorners { get; set; }
        public int AwayCorners { get; set; }

        public int DurationMinutes { get; set; }
        public bool Finished { get; set; }

       
     public IEnumerable<PlayerMatchStatsDTO> PlayerStats { get; set; } = new List<PlayerMatchStatsDTO>();
     
    }
}
