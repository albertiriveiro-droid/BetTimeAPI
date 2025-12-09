public class MatchSimulationHelper
{
    public class MatchResult
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public List<GolEvent> GoalEvents { get; set; } = new List<GolEvent>();
    }

    public class GolEvent
    {
        public int Minute { get; set; }
        public int TeamId { get; set; }
    }

    public static MatchResult SimulateMatch(decimal homeOdds, decimal drawOdds, decimal awayOdds, int durationMinutes, int homeTeamId, int awayTeamId)
    {
        // Probabilidades
        double homeProb = 1.0 / (double)homeOdds;
        double drawProb = 1.0 / (double)drawOdds;
        double awayProb = 1.0 / (double)awayOdds;
        double total = homeProb + drawProb + awayProb;
        homeProb /= total;
        drawProb /= total;
        awayProb /= total;

        Random rnd = new Random();

       
        int maxGoals = 5;
        int homeGoals = (int)Math.Round(homeProb * maxGoals * rnd.NextDouble());
        int awayGoals = (int)Math.Round(awayProb * maxGoals * rnd.NextDouble());

        List<GolEvent> events = new List<GolEvent>();

      
        for (int i = 0; i < homeGoals; i++)
        {
            events.Add(new GolEvent { Minute = rnd.Next(1, durationMinutes + 1), TeamId = homeTeamId });
        }
        for (int i = 0; i < awayGoals; i++)
        {
            events.Add(new GolEvent { Minute = rnd.Next(1, durationMinutes + 1), TeamId = awayTeamId });
        }

       
        events = events.OrderBy(e => e.Minute).ToList();

        return new MatchResult
        {
            HomeScore = homeGoals,
            AwayScore = awayGoals,
            GoalEvents = events
        };
    }
}