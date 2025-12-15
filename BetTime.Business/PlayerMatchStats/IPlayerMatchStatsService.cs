using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Services
{
    public interface IPlayerMatchStatsService
    {
        PlayerMatchStats CreatePlayerMatchStats(PlayerMatchStatsDTO playerStatsDto);
        PlayerMatchStats? GetPlayerMatchStats(int playerId, int matchId);
        IEnumerable<PlayerMatchStats> GetPlayerMatchStatsByMatch(int matchId);
        PlayerMatchStats UpdatePlayerMatchStats(int playerId, int matchId, PlayerMatchStatsUpdateDTO playerStatsUpdateDto);
    }
}