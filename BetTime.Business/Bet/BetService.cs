using BetTime.Models;
using BetTime.Data;

namespace BetTime.Business;

public class BetService : IBetService
{
    private readonly IBetRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IMatchRepository _matchRepository;

    public BetService(IBetRepository repository, IUserRepository userRepository, IMatchRepository matchRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _matchRepository = matchRepository;
    }

   
 public Bet CreateBet(BetCreateDTO betCreateDTO)
{
    var user = _userRepository.GetUserById(betCreateDTO.UserId)
        ?? throw new KeyNotFoundException($"User with ID {betCreateDTO.UserId} not found");

    var selection = _matchRepository
        .GetAllMatches() 
        .SelectMany(m => m.Markets)
        .SelectMany(m => m.Selections)
        .FirstOrDefault(s => s.Id == betCreateDTO.MarketSelectionId);

    if (selection == null)
        throw new KeyNotFoundException($"Market selection with ID {betCreateDTO.MarketSelectionId} not found");

    
    var market = selection.Market ?? throw new KeyNotFoundException("Associated market not found");
    var match = market.Match ?? throw new KeyNotFoundException("Associated match not found");

    // 3️⃣ Validaciones
    if (match.Finished)
        throw new InvalidOperationException("Cannot bet on a finished match.");

    if (match.StartTime <= DateTime.UtcNow)
        throw new InvalidOperationException("Cannot bet after match has started.");

    if (betCreateDTO.Amount <= 0)
        throw new ArgumentException("Bet amount must be greater than 0.");

    if (user.Balance < betCreateDTO.Amount)
        throw new InvalidOperationException("Insufficient balance.");

  
    user.Balance -= betCreateDTO.Amount;
    _userRepository.UpdateUser(user);

    
    var bet = new Bet
    {
        UserId = user.Id,
        MatchId = match.Id,
        MarketSelectionId = selection.Id,
        Amount = betCreateDTO.Amount,
        PlacedAt = DateTime.UtcNow
    };

    _repository.AddBet(bet);
    return bet;
}

    public Bet ResolveBet(int betId)
    {
        var bet = _repository.GetBetById(betId)
            ?? throw new KeyNotFoundException($"Bet with ID {betId} not found");

        var match = bet.Match ?? throw new KeyNotFoundException("Associated match not found");
        var selection = bet.MarketSelection ?? throw new KeyNotFoundException("Selection not found for this bet");

        if (!match.Finished)
            throw new InvalidOperationException("Cannot resolve bet before match ends.");

     
        bet.Won = MarketResolver.Resolve(match, selection);

        if (bet.Won == true)
        {
            var user = _userRepository.GetUserById(bet.UserId)
                ?? throw new KeyNotFoundException($"User with ID {bet.UserId} not found");

            user.Balance += bet.Amount * selection.Odd; 
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
            var selection = bet.MarketSelection;
            if (selection == null) continue;

            bet.Won = MarketResolver.Resolve(match, selection);

            if (bet.Won == true)
            {
                var user = _userRepository.GetUserById(bet.UserId)
                    ?? throw new KeyNotFoundException($"User with ID {bet.UserId} not found");

                user.Balance += bet.Amount * selection.Odd;
                _userRepository.UpdateUser(user);
            }

            _repository.UpdateBet(bet);
        }
    }

    // Consultas generales
    public Bet GetBetById(int betId)
    {
        return _repository.GetBetById(betId)
            ?? throw new KeyNotFoundException($"Bet with ID {betId} not found");
    }

    public IEnumerable<Bet> GetAllBets() => _repository.GetAllBets();
    public IEnumerable<Bet> GetBetsByUser(int userId) => _repository.GetBetsByUser(userId);
    public IEnumerable<Bet> GetBetsByMatch(int matchId) => _repository.GetBetsByMatch(matchId);
    public IEnumerable<Bet> GetActiveBets() => _repository.GetActiveBets();
    public IEnumerable<Bet> GetFinishedBets() => _repository.GetFinishedBets();

    public IEnumerable<BetOutputDTO> GetWonBets(int userId) =>
        _repository.GetWonBets(userId).Select(MapToDTO);

    public IEnumerable<BetOutputDTO> GetLostBets(int userId) =>
        _repository.GetLostBets(userId).Select(MapToDTO);

    private BetOutputDTO MapToDTO(Bet bet) => new()
    {
        Id = bet.Id,
        MatchId = bet.MatchId,
        AmountBet = bet.Amount,
        Won = bet.Won,
        AmountWon = bet.Won == true && bet.MarketSelection != null ? bet.Amount * bet.MarketSelection.Odd : 0,
        Date = bet.PlacedAt
    };
}