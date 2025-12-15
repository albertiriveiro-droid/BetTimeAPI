using BetTime.Models;
namespace BetTime.Data;


public interface IPlayerMatchStatsRepository
{
    PlayerMatchStats? GetPlayerMatchStatsByPlayerAndMatch(int playerId, int matchId);
    IEnumerable<PlayerMatchStats> GetPlayerMatchStatsByMatch(int matchId);
    void AddPlayerMatchStats(PlayerMatchStats stats);
    void UpdatePlayerMatchStats(PlayerMatchStats stats);
    void SaveChanges();
}