using BetTime.Models;
using BetTime.Data;

namespace BetTime.Business;

public class PlayerMarketGeneratorService : IPlayerMarketGeneratorService
{
    private readonly IPlayerMarketService _playerMarketService;
    private readonly IPlayerMarketSelectionService _selectionService;
    private readonly IPlayerRepository _playerRepository;
    private readonly DynamicOddsService _dynamicOddsService;

    public PlayerMarketGeneratorService(
        IPlayerMarketService playerMarketService,
        IPlayerMarketSelectionService selectionService,
        IPlayerRepository playerRepository,
        DynamicOddsService dynamicOddsService)
    {
        _playerMarketService = playerMarketService;
        _selectionService = selectionService;
        _playerRepository = playerRepository;
        _dynamicOddsService = dynamicOddsService;
    }

    public List<PlayerMarket> GenerateMarketsForPlayer(int matchId, int playerId)
    {
        var createdMarkets = new List<PlayerMarket>();

        var player = _playerRepository.GetPlayerById(playerId)
            ?? throw new KeyNotFoundException($"Player {playerId} not found");

        var marketTypes = new[]
        {
            PlayerMarketType.Goal,
            PlayerMarketType.Assist,
            PlayerMarketType.YellowCard,
            PlayerMarketType.RedCard
        };

        foreach (var type in marketTypes)
        {
            var dto = new PlayerMarketCreateDTO
            {
                PlayerId = player.Id,
                PlayerMarketType = type
            };

            var market = _playerMarketService.CreatePlayerMarket(matchId, dto);
            createdMarkets.Add(market);

            var odd = _dynamicOddsService.GeneratePlayerOdd(
                type,
                player.Position 
            );

            var selection = new PlayerMarketSelectionCreateDTO
            {
                Name = "Sí",
                Odds = odd
            };

            _selectionService.CreatePlayerSelection(market.Id, selection);
        }

        return createdMarkets;
    }
}
