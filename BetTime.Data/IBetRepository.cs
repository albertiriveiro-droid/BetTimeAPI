using BetTime.Models;

namespace BetTime.Data;

public interface IBetRepository
{
    void AddBet(Bet bet);
    IEnumerable<Bet> GetAllBets();
    Bet GetBetById(int betId);
    IEnumerable<Bet> GetBetsByUser(int userId);
    IEnumerable<Bet> GetBetsByMatch(int matchId);
    IEnumerable<Bet> GetActiveBets();
    IEnumerable<Bet> GetFinishedBets();
    IEnumerable<Bet> GetWonBets(int userId);
    IEnumerable<Bet> GetLostBets(int userId);
    void UpdateBet(Bet bet);

    void SaveChanges();
}
