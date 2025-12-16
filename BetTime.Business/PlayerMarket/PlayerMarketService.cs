using BetTime.Data;
using BetTime.Models;
using System;
using System.Collections.Generic;

namespace BetTime.Services
{
    public class PlayerMarketService : IPlayerMarketService
    {
        private readonly IPlayerMarketRepository _repository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerMarketService(IPlayerMarketRepository repository, IMatchRepository matchRepository,   IPlayerRepository playerRepository)
        {
            _repository = repository;
            _matchRepository= matchRepository;
             _playerRepository = playerRepository;  
        }

       public PlayerMarket CreatePlayerMarket(int matchId, PlayerMarketCreateDTO dto)
{
    
    var match = _matchRepository.GetMatchById(matchId)
        ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

   
    if (match.Finished)
        throw new InvalidOperationException("Cannot create PlayerMarkets for finished match");

    if (match.StartTime <= DateTime.UtcNow)
        throw new InvalidOperationException("Cannot create PlayerMarkets after match start");
        
    var playersInMatch = _playerRepository.GetPlayerByTeam(match.HomeTeamId)
    .Concat(_playerRepository.GetPlayerByTeam(match.AwayTeamId))
                        .Select(p => p.Id)
                        .ToHashSet();

    if (!playersInMatch.Contains(dto.PlayerId))
        throw new InvalidOperationException("Cannot create PlayerMarket: player is not part of this match");

    var playerMarket = new PlayerMarket
    {
        MatchId = matchId, 
        PlayerId = dto.PlayerId,
        PlayerMarketType = dto.PlayerMarketType,
        CreatedAt = DateTime.UtcNow,
        IsOpen = true
    };

    _repository.AddPlayerMarket(playerMarket);
    return playerMarket;
}
        public PlayerMarket GetPlayerMarketById(int id)
        {
            var market = _repository.GetPlayerMarketById(id);
            if (market == null)
                throw new KeyNotFoundException($"PlayerMarket with id {id} not found");
            return market;
        }

        public IEnumerable<PlayerMarket> GetPlayerMarketsByMatch(int matchId)
        {
            if (matchId <= 0)
                throw new InvalidOperationException("Invalid MatchId");

            var markets = _repository.GetPlayerMarketsByMatch(matchId);
            if (markets == null)
                throw new KeyNotFoundException($"No PlayerMarkets found for match {matchId}");

            return markets;
        }
    }
}
