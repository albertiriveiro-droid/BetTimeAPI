using BetTime.Models;

public interface IBetService
{
    Bet CreateBet(BetCreateDTO dto);
    Bet GetBetById(int betId);

    IEnumerable<Bet> GetAllBets();
    IEnumerable<BetOutputDTO> GetBetsByUser(int userId);
    IEnumerable<Bet> GetBetsByMatch(int matchId);

    IEnumerable<BetOutputDTO> GetActiveBets(int userId);
    IEnumerable<BetOutputDTO> GetFinishedBets(int userId);
    IEnumerable<BetOutputDTO> GetWonBets(int userId);
    IEnumerable<BetOutputDTO> GetLostBets(int userId);

    Bet ResolveBet(int betId);               
    void ResolveBetsForMatch(int matchId);   
}