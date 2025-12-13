
using BetTime.Models;
namespace BetTime.Business
{
    public class MatchSimulationHelper
    {
        private static readonly Random rnd = new Random();

        public class MatchResult
        {
            public int HomeScore { get; set; }
            public int AwayScore { get; set; }
            public List<GolEvent> GoalEvents { get; set; } = new List<GolEvent>();

            public int HomeCorners { get; set; }
            public int AwayCorners { get; set; }

            public OneXTwoSelection OneXTwoOutcome =>
                HomeScore > AwayScore ? OneXTwoSelection.Home :
                HomeScore < AwayScore ? OneXTwoSelection.Away :
                OneXTwoSelection.Draw;

            public decimal TotalGoals => HomeScore + AwayScore;
            public int TotalCorners => HomeCorners + AwayCorners;
        }

        public class GolEvent
        {
            public int Minute { get; set; }
            public int TeamId { get; set; }
        }

        public static MatchResult SimulateMatch(int homeTeamId, int awayTeamId, int durationMinutes)
        {
           
            int maxGoals = 5;
            int homeGoals = rnd.Next(0, maxGoals + 1);
            int awayGoals = rnd.Next(0, maxGoals + 1);

            List<GolEvent> goalEvents = new List<GolEvent>();
            for (int i = 0; i < homeGoals; i++)
                goalEvents.Add(new GolEvent { Minute = rnd.Next(1, durationMinutes + 1), TeamId = homeTeamId });

            for (int i = 0; i < awayGoals; i++)
                goalEvents.Add(new GolEvent { Minute = rnd.Next(1, durationMinutes + 1), TeamId = awayTeamId });

            goalEvents = goalEvents.OrderBy(e => e.Minute).ToList();

           
            int homeCorners = rnd.Next(2, 11);
            int awayCorners = rnd.Next(2, 11);

            return new MatchResult
            {
                HomeScore = homeGoals,
                AwayScore = awayGoals,
                GoalEvents = goalEvents,
                HomeCorners = homeCorners,
                AwayCorners = awayCorners
            };
        }
    }
}