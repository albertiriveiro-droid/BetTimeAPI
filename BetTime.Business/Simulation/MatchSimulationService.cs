using BetTime.Data;
using BetTime.Models;
using BetTime.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BetTime.Business
{
    public class MatchSimulationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly Random rnd = new Random();

        public MatchSimulationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    var matchRepository = scope.ServiceProvider.GetRequiredService<IMatchRepository>();
                    var playerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();
                    var playerMatchStatsService = scope.ServiceProvider.GetRequiredService<IPlayerMatchStatsService>();
                    var betService = scope.ServiceProvider.GetRequiredService<IBetService>(); // 🔥 CLAVE

                    var pendingMatches = matchRepository.GetPendingMatches(DateTime.UtcNow);

                    foreach (var match in pendingMatches)
                    {
                        var homePlayers = playerService
                            .GetPlayersByTeam(match.HomeTeamId)
                            .Where(p => p.IsActive)
                            .ToList();

                        var awayPlayers = playerService
                            .GetPlayersByTeam(match.AwayTeamId)
                            .Where(p => p.IsActive)
                            .ToList();

                        int homeGoals = rnd.Next(0, 6);
                        int awayGoals = rnd.Next(0, 6);

                        var result = MatchSimulationHelper.SimulateMatch(
                            homePlayers,
                            awayPlayers,
                            homeGoals,
                            awayGoals,
                            match.DurationMinutes
                        );

                        match.HomeScore = result.HomeScore;
                        match.AwayScore = result.AwayScore;
                        match.HomeCorners = result.HomeCorners;
                        match.AwayCorners = result.AwayCorners;
                        match.Finished = true;

                        foreach (var player in homePlayers.Concat(awayPlayers))
                        {
                            var stats = playerMatchStatsService.GetPlayerMatchStats(player.Id, match.Id)
                                ?? new PlayerMatchStats(player.Id, match.Id);

                            var updateDto = new PlayerMatchStatsUpdateDTO
                            {
                                Goals = stats.Goals + player.TempGoals,
                                Assists = stats.Assists + player.TempAssists,
                                YellowCards = stats.YellowCard + player.TempYellowCards,
                                RedCards = stats.RedCard + player.TempRedCards,
                                MinutesPlayed = stats.MinutesPlayed + player.TempMinutesPlayed
                            };

                            playerMatchStatsService.UpdatePlayerMatchStats(player.Id, match.Id, updateDto);

                            player.TempGoals = 0;
                            player.TempAssists = 0;
                            player.TempYellowCards = 0;
                            player.TempRedCards = 0;
                            player.TempMinutesPlayed = 0;
                        }

                        match.PlayerMatchStats =
                            playerMatchStatsService.GetPlayerMatchStatsByMatch(match.Id).ToList();

                        match.PlayerMatchStatsJson = JsonSerializer.Serialize(
                            match.PlayerMatchStats.Select(s => new
                            {
                                s.PlayerId,
                                s.Goals,
                                s.Assists,
                                s.YellowCard,
                                s.RedCard,
                                s.MinutesPlayed
                            })
                        );

                        matchRepository.UpdateMatch(match);

                        betService.ResolveBetsForMatch(match.Id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error simulando partidos: {ex}");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}