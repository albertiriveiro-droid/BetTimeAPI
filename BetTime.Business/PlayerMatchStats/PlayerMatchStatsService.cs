using BetTime.Models;
using BetTime.Data;
using System.Collections.Generic;

namespace BetTime.Services
{
    public class PlayerMatchStatsService : IPlayerMatchStatsService
    {
        private readonly IPlayerMatchStatsRepository _playerMatchStatsRepository;

        public PlayerMatchStatsService(IPlayerMatchStatsRepository playerMatchStatsRepository)
        {
            _playerMatchStatsRepository = playerMatchStatsRepository;
        }

        public PlayerMatchStats CreatePlayerMatchStats(PlayerMatchStatsDTO playerStatsDto)
        {
            var playerStats = new PlayerMatchStats(playerStatsDto.PlayerId, playerStatsDto.MatchId)
            {
                Goals = playerStatsDto.Goals,
                Assists = playerStatsDto.Assists,
                MinutesPlayed = playerStatsDto.MinutesPlayed
            };
            _playerMatchStatsRepository.AddPlayerMatchStats(playerStats);
            return playerStats;
        }

        public PlayerMatchStats? GetPlayerMatchStats(int playerId, int matchId)
            => _playerMatchStatsRepository.GetPlayerMatchStatsByPlayerAndMatch(playerId, matchId);

        public IEnumerable<PlayerMatchStats> GetPlayerMatchStatsByMatch(int matchId)
            => _playerMatchStatsRepository.GetPlayerMatchStatsByMatch(matchId);

        public PlayerMatchStats UpdatePlayerMatchStats(int playerId, int matchId, PlayerMatchStatsUpdateDTO playerStatsUpdateDto)
        {
            var playerStats = _playerMatchStatsRepository.GetPlayerMatchStatsByPlayerAndMatch(playerId, matchId);
            if (playerStats == null) return null;

            if (playerStatsUpdateDto.Goals.HasValue) playerStats.Goals = playerStatsUpdateDto.Goals.Value;
            if (playerStatsUpdateDto.Assists.HasValue) playerStats.Assists = playerStatsUpdateDto.Assists.Value;
            if (playerStatsUpdateDto.MinutesPlayed.HasValue) playerStats.MinutesPlayed = playerStatsUpdateDto.MinutesPlayed.Value;

            _playerMatchStatsRepository.UpdatePlayerMatchStats(playerStats);
            return playerStats;
        }
    }
}