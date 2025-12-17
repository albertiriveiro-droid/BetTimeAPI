using BetTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetTime.Business
{
    public class MatchSimulationHelper
    {
        private static readonly Random rnd = new Random();

        public class MatchResult
        {
            public int HomeScore { get; set; }
            public int AwayScore { get; set; }
            public int HomeCorners { get; set; }
            public int AwayCorners { get; set; }

            public OneXTwoSelection OneXTwoOutcome =>
                HomeScore > AwayScore ? OneXTwoSelection.Home :
                HomeScore < AwayScore ? OneXTwoSelection.Away :
                OneXTwoSelection.Draw;

            public decimal TotalGoals => HomeScore + AwayScore;
            public int TotalCorners => HomeCorners + AwayCorners;
        }

        public static MatchResult SimulateMatch(
            List<Player> homePlayers,
            List<Player> awayPlayers,
            int homeGoals,
            int awayGoals,
            int durationMinutes)
        {
            var result = new MatchResult
            {
                HomeScore = homeGoals,
                AwayScore = awayGoals,
                HomeCorners = rnd.Next(2, 11),
                AwayCorners = rnd.Next(2, 11)
            };

          
            void GeneratePlayerStats(List<Player> players, int goals)
            {
                if (players == null || players.Count == 0) return;

                for (int i = 0; i < goals; i++)
                {
                    var scorer = players[rnd.Next(players.Count)];
                    var assisterCandidates = players.Where(p => p.Id != scorer.Id).ToList();
                    var assister = assisterCandidates.Count > 0 ? assisterCandidates[rnd.Next(assisterCandidates.Count)] : null;

                  
                    scorer.TempGoals += 1;
                    if (assister != null) assister.TempAssists += 1;
                }

             
                foreach (var p in players)
                {
                    
                 if (rnd.NextDouble() < 0.20)
                p.TempYellowCards += 1;

       
                if (rnd.NextDouble() < 0.02)
                p.TempRedCards += 1;
                p.TempMinutesPlayed = durationMinutes;
                }
            }

            GeneratePlayerStats(homePlayers, homeGoals);
            GeneratePlayerStats(awayPlayers, awayGoals);

            return result;
        }
    }
}