
using BetTime.Data;
using BetTime.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BetTime.Business
{
    public class MatchSimulationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

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
                    var betService = scope.ServiceProvider.GetRequiredService<IBetService>();

                    var pendingMatches = matchRepository.GetPendingMatches(DateTime.UtcNow);

                    foreach (var match in pendingMatches)
                    {
                        var result = MatchSimulationHelper.SimulateMatch(
                            match.HomeTeamId,
                            match.AwayTeamId,
                            match.DurationMinutes
                        );

                        match.HomeScore = result.HomeScore;
                        match.AwayScore = result.AwayScore;

                        match.HomeCorners = result.HomeCorners;
                        match.AwayCorners = result.AwayCorners;

                        match.Finished = true;

                        matchRepository.UpdateMatch(match);

                        betService.ResolveBetsForMatch(match.Id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error simulando partidos: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}