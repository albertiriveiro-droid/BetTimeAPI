using BetTime.Models;

public interface IBetService
{
    Bet CreateBet(BetCreateDTO dto);
    Bet GetBetById(int betId);

    IEnumerable<Bet> GetAllBets();
    IEnumerable<Bet> GetBetsByUser(int userId);
    IEnumerable<Bet> GetBetsByMatch(int matchId);

    IEnumerable<Bet> GetActiveBets();
    IEnumerable<Bet> GetFinishedBets();
    IEnumerable<BetOutputDTO> GetWonBets(int userId);
    IEnumerable<BetOutputDTO> GetLostBets(int userId);

    Bet ResolveBet(int betId);               
    void ResolveBetsForMatch(int matchId);   
}