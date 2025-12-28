using BetTime.Models;
using BetTime.Data;
using BetTime.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetTime.Business
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerMarketSelectionService _playerMarketSelectionService;
       private readonly IMarketSelectionRepository _marketSelectionRepository;

        public BetService(
            IBetRepository repository,
            IUserRepository userRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            IPlayerMarketSelectionService playerMarketSelectionService,
            IMarketSelectionRepository marketSelectionRepository)

        {
            _repository = repository;
            _userRepository = userRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _playerMarketSelectionService = playerMarketSelectionService;
            _marketSelectionRepository = marketSelectionRepository; 
        }

      public Bet CreateBet(BetCreateDTO dto)
{
    var user = _userRepository.GetUserById(dto.UserId)
               ?? throw new KeyNotFoundException($"User {dto.UserId} not found");

    bool hasMarketSelection = dto.MarketSelectionId.HasValue;
    bool hasPlayerSelection = dto.PlayerMarketSelectionId.HasValue;

    if (hasMarketSelection && hasPlayerSelection)
        throw new InvalidOperationException("Cannot bet on both market and player at the same time.");
    if (!hasMarketSelection && !hasPlayerSelection)
        throw new InvalidOperationException("Must bet on market or player.");

    int matchId;
    int? marketSelectionId = null;
    int? playerMarketSelectionId = null;
    decimal odd = 1m;

    if (hasMarketSelection)
    {
      
        var selection = _marketSelectionRepository.GetSelectionById(dto.MarketSelectionId.Value)
                        ?? throw new KeyNotFoundException("Market selection not found");

        marketSelectionId = selection.Id;
        matchId = selection.Market.MatchId; 
        odd = selection.Odd;

        
        var match = _matchRepository.GetMatchWithMarketsAndSelections(matchId);

        if (match.Finished) throw new InvalidOperationException("Cannot bet on finished match");
        if (match.StartTime <= DateTime.UtcNow) throw new InvalidOperationException("Cannot bet after match start");
    }
    else
    {
        var selection = _playerMarketSelectionService.GetPlayerSelectionById(dto.PlayerMarketSelectionId.Value)
                        ?? throw new KeyNotFoundException("Player market selection not found");

        playerMarketSelectionId = selection.Id;
        matchId = selection.PlayerMarket.MatchId;
        odd = selection.Odd;

        var match = _matchRepository.GetMatchWithMarketsAndSelections(matchId);

        if (match.Finished) throw new InvalidOperationException("Cannot bet on finished match");
        if (match.StartTime <= DateTime.UtcNow) throw new InvalidOperationException("Cannot bet after match start");
    }

    if (dto.Amount <= 0) throw new ArgumentException("Amount must be greater than 0");
    if (user.Balance < dto.Amount) throw new InvalidOperationException("Insufficient balance");

    user.Balance -= dto.Amount;
    _userRepository.UpdateUser(user);

    var bet = new Bet
    {
        UserId = user.Id,
        MatchId = matchId,
        MarketSelectionId = marketSelectionId,
        PlayerMarketSelectionId = playerMarketSelectionId,
        Amount = dto.Amount,
        PlacedAt = DateTime.UtcNow
    };

    _repository.AddBet(bet);
    return bet;
}

public Bet ResolveBet(int betId)
{
    var bet = _repository.GetBetById(betId)
              ?? throw new KeyNotFoundException("Bet not found");

    var match = _matchRepository.GetMatchWithMarketsAndSelections(bet.MatchId)
                ?? throw new KeyNotFoundException("Match not found");

    if (!match.Finished)
        throw new InvalidOperationException("Match not finished");

    decimal odd;

    if (bet.MarketSelectionId.HasValue)
    {
        var selection = match.Markets
            .SelectMany(m => m.Selections)
            .FirstOrDefault(s => s.Id == bet.MarketSelectionId)
            ?? throw new KeyNotFoundException("Market selection not found");

        bet.Won = MarketResolver.Resolve(match, selection);
        odd = selection.Odd;
    }
    else if (bet.PlayerMarketSelectionId.HasValue)
    {
        var selection = _playerMarketSelectionService.GetPlayerSelectionById(bet.PlayerMarketSelectionId.Value)
                        ?? throw new KeyNotFoundException("Player market selection not found");

        bet.Won = PlayerMarketResolver.Resolve(match, selection, _playerRepository);
        odd = selection.Odd;
    }
    else
    {
        throw new InvalidOperationException("Invalid bet: no selection");
    }

    if (bet.Won == true)
    {
        var user = _userRepository.GetUserById(bet.UserId)
                   ?? throw new KeyNotFoundException("User not found");

        user.Balance += bet.Amount * odd;
        _userRepository.UpdateUser(user);
    }

    _repository.UpdateBet(bet);
    return bet;
}


public void ResolveBetsForMatch(int matchId)
{
    var match = _matchRepository.GetMatchById(matchId)
        ?? throw new KeyNotFoundException("Match not found");

    if (!match.Finished)
        throw new InvalidOperationException("Match not finished");

    var bets = _repository.GetBetsByMatch(matchId);

    foreach (var bet in bets)
    {
        ResolveBet(bet.Id);
    }
}


        public Bet GetBetById(int betId) => _repository.GetBetById(betId)
                                               ?? throw new KeyNotFoundException("Bet not found");

        public IEnumerable<Bet> GetAllBets() => _repository.GetAllBets();
       public IEnumerable<BetOutputDTO> GetBetsByUser(int userId)
        {
         return _repository.GetBetsByUser(userId).Select(MapToDTO);
        }
        public IEnumerable<Bet> GetBetsByMatch(int matchId) => _repository.GetBetsByMatch(matchId);

        public IEnumerable<BetOutputDTO> GetWonBets(int userId) =>
            _repository.GetWonBets(userId).Select(MapToDTO);

        public IEnumerable<BetOutputDTO> GetLostBets(int userId) =>
            _repository.GetLostBets(userId).Select(MapToDTO);

        public IEnumerable<BetOutputDTO> GetActiveBets(int userId)
        {
        return _repository
        .GetBetsByUser(userId)
        .Where(b => !b.Won.HasValue)
        .Select(MapToDTO);
        }
        public IEnumerable<BetOutputDTO> GetFinishedBets(int userId)
        {
        return _repository
        .GetBetsByUser(userId)
        .Where(b => b.Won.HasValue)
        .Select(MapToDTO);
        }

        private BetOutputDTO MapToDTO(Bet bet)
        {
            string? playerName = null;
            string? playerMarketType = null;

            if (bet.PlayerMarketSelectionId.HasValue)
            {
                var selection = _playerMarketSelectionService
                    .GetSelectionByPlayerId(bet.PlayerMarketSelectionId.Value);

                if (selection != null)
                {
                    playerName = _playerRepository.GetPlayerById(selection.PlayerMarket.PlayerId)?.Name;
                    playerMarketType = selection.PlayerMarket.PlayerMarketType.ToString();
                }
            }

            string? marketType = null;
            string? selectionName = null;

            if (bet.MarketSelectionId != 0 && bet.MarketSelection != null)
            {
                marketType = bet.MarketSelection.Market?.MarketType.ToString();
                selectionName = bet.MarketSelection.Name;
            }

            decimal odds = bet.MarketSelection?.Odd ??
                           (bet.PlayerMarketSelectionId.HasValue
                            ? _playerMarketSelectionService
                                .GetSelectionByPlayerId(bet.PlayerMarketSelectionId.Value).Odd
                            : 1m);

            return new BetOutputDTO
            {
                Id = bet.Id,
                MatchId = bet.MatchId,
                MarketType = marketType,
                SelectionName = selectionName,
                PlayerName = playerName,
                PlayerMarketType = playerMarketType,
                AmountBet = bet.Amount,
                Odds = odds,
                Won = bet.Won,
                AmountWon = bet.Won == true ? bet.Amount * odds : 0,
                Date = bet.PlacedAt
            };
        }
    }
}