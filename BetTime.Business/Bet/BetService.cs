using BetTime.Models;
using BetTime.Data;

namespace BetTime.Business;
public class BetService : IBetService
{
    private readonly IBetRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IMatchRepository _matchRepository;

    public BetService(
        IBetRepository repository,
        IUserRepository userRepository,
        IMatchRepository matchRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _matchRepository = matchRepository;
    }

public Bet CreateBet(BetCreateDTO betCreateDTO)
    {
    
var user= _userRepository.GetUserById(betCreateDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {betCreateDTO.UserId} not found");
        }
    var match= _matchRepository.GetMatchById(betCreateDTO.MatchId);
     if (match == null)
        {
            throw new KeyNotFoundException($"Match with ID {betCreateDTO.MatchId} not found");
        }

    if (match.Finished)
            throw new InvalidOperationException("Cannot bet on a finished match.");
        
    if (match.StartTime <= DateTime.UtcNow)
        throw new InvalidOperationException("Cannot bet after match has started.");

     if (betCreateDTO.Amount <= 0)
            throw new ArgumentException("Bet amount must be greater than 0.");   

    string prediction = betCreateDTO.Prediction.ToUpper();
        if (prediction != "HOME" && prediction != "AWAY" && prediction != "DRAW")
            throw new ArgumentException("Prediction must be HOME, DRAW or AWAY.");

        decimal odds = prediction switch
        {
            "HOME" => match.HomeOdds,
            "DRAW" => match.DrawOdds,
            "AWAY" => match.AwayOdds,
            _ => throw new Exception("Unexpected prediction")
        };
        var bet= new Bet(betCreateDTO.UserId, betCreateDTO.MatchId, betCreateDTO.Amount, odds, prediction);
        _repository.AddBet(bet);
        return bet;
    }

    public Bet GetBetById(int betId)
    {
        var bet= _repository.GetBetById(betId);
        if (bet == null)
        {
           throw new KeyNotFoundException($"Bet with ID {betId} not found");  
        }
        return bet;
    }

    public IEnumerable<Bet> GetAllBets()
    {
        return _repository.GetAllBets();
    }

    public IEnumerable<Bet> GetBetsByUser( int userId)
    {
     return _repository.GetBetsByUser(userId);

    }

    public IEnumerable<Bet> GetBetsByMatch (int matchId)
    {
        return _repository.GetBetsByMatch(matchId);

    }

    public IEnumerable<Bet> GetActiveBets()
    {
        return  _repository.GetActiveBets();  
    } 

    public IEnumerable<Bet> GetFinishedBets()
    {
        return _repository.GetFinishedBets();  
    } 

   public IEnumerable<BetOutputDTO> GetWonBets(int userId)
{
        return _repository
            .GetWonBets(userId)
            .Select(b => MapToDTO(b));
}
   public IEnumerable<BetOutputDTO> GetLostBets(int userId)
{
        return _repository
            .GetLostBets(userId)
            .Select(b => MapToDTO(b));
}
    public Bet ResolveBet(int betId)
    {
        var bet = _repository.GetBetById(betId)
            ?? throw new KeyNotFoundException($"Bet with ID {betId} not found");

        var match = bet.Match;

        if (!match.Finished)
            throw new InvalidOperationException("Cannot resolve bet before match ends.");

        string realOutcome =
            match.HomeScore > match.AwayScore ? "HOME" :
            match.HomeScore < match.AwayScore ? "AWAY" :
            "DRAW";

        bet.Won = bet.Prediction == realOutcome;
        if (bet.Won == true)
    {
        var user = _userRepository.GetUserById(bet.UserId)
            ?? throw new KeyNotFoundException($"User with ID {bet.UserId} not found");

        user.Balance += bet.Amount * bet.Odds;
        _userRepository.UpdateUser(user);
    }

        _repository.UpdateBet(bet);

        return bet;
    }

    public void ResolveBetsForMatch(int matchId)
    {
        var match = _matchRepository.GetMatchById(matchId)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (!match.Finished)
            throw new InvalidOperationException("Match must be finished before resolving bets.");

        var bets = _repository.GetBetsByMatch(matchId);

        foreach (var bet in bets)
        {
            string realOutcome =
                match.HomeScore > match.AwayScore ? "HOME" :
                match.HomeScore < match.AwayScore ? "AWAY" :
                "DRAW";

            bet.Won = bet.Prediction == realOutcome;
             if (bet.Won == true)
            {
            var user = _userRepository.GetUserById(bet.UserId)
            ?? throw new KeyNotFoundException($"User with ID {bet.UserId} not found");

            user.Balance += bet.Amount * bet.Odds;
            _userRepository.UpdateUser(user);
    }


            _repository.UpdateBet(bet);
        }
    }

    private BetOutputDTO MapToDTO(Bet bet)
{
    return new BetOutputDTO
    {
        Id = bet.Id,
        MatchId = bet.MatchId,
        Prediction = bet.Prediction,
        AmountBet = bet.Amount,
        Odds = bet.Odds,
        Won = bet.Won,
        AmountWon = bet.Won == true ? bet.Amount * bet.Odds : 0,
        Date = bet.PlacedAt
    };
}

}