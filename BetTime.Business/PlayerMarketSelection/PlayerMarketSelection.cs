using BetTime.Data;
using BetTime.Models;
using System;
using System.Collections.Generic;

namespace BetTime.Services
{
   public class PlayerMarketSelectionService : IPlayerMarketSelectionService
{
    private readonly IPlayerMarketSelectionRepository _selectionRepository;
    private readonly IPlayerMarketRepository _playerMarketRepository;

    public PlayerMarketSelectionService(
        IPlayerMarketSelectionRepository selectionRepository,
        IPlayerMarketRepository playerMarketRepository)
    {
        _selectionRepository = selectionRepository;
        _playerMarketRepository = playerMarketRepository;
    }
        public PlayerMarketSelection CreatePlayerSelection(int playerMarketId, PlayerMarketSelectionCreateDTO dto)
        {
            
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Selection DTO cannot be null");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new InvalidOperationException("Selection must have a name");

            if (dto.Odds <= 0)
                throw new InvalidOperationException("Selection odds must be greater than zero");

            var playerMarket = _playerMarketRepository.GetPlayerMarketById(playerMarketId)
                ?? throw new KeyNotFoundException($"PlayerMarket with ID {playerMarketId} not found");

           
            if (!playerMarket.IsOpen)
                throw new InvalidOperationException("Cannot add selections to a closed PlayerMarket");

            var selection = new PlayerMarketSelection
            {
                PlayerMarketId = playerMarketId,
                Name = dto.Name,
                Odd = dto.Odds
            };

            _selectionRepository.AddPlayerSelection(selection);

            return selection;
        }

        public PlayerMarketSelection GetSelectionByPlayerId(int playerId)
        {
            var selection = _selectionRepository.GetSelectionByPlayerId(playerId);
            if (selection == null)
                throw new KeyNotFoundException($"PlayerMarketSelection for player {playerId} not found");

            return selection;
        }

 
        public IEnumerable<PlayerMarketSelection> GetSelectionsByPlayerMarket(int playerMarketId)
        {
            if (playerMarketId <= 0)
                throw new InvalidOperationException("Invalid PlayerMarketId");

            var selections = _selectionRepository.GetSelectionsByPlayerMarket(playerMarketId);
            if (selections == null)
                throw new KeyNotFoundException($"No selections found for PlayerMarket {playerMarketId}");

            return selections;
        }

            public PlayerMarketSelection GetPlayerSelectionById(int selectionId)
            {
        var selection = _selectionRepository.GetSelectionById(selectionId);
        if (selection == null)
         throw new KeyNotFoundException($"PlayerMarketSelection with ID {selectionId} not found");
         return selection;
        }
    }
}