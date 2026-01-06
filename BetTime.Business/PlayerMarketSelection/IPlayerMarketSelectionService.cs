using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Business
{
    public interface IPlayerMarketSelectionService
    {
        PlayerMarketSelection CreatePlayerSelection(int playerMarketId, PlayerMarketSelectionCreateDTO playerMarketSelectionCreateDTO);
        PlayerMarketSelection GetPlayerSelectionById( int selectionId);
        PlayerMarketSelection? GetSelectionByPlayerId(int playerId);
        IEnumerable<PlayerMarketSelection> GetSelectionsByPlayerMarket(int playerMarketId);
    }
}