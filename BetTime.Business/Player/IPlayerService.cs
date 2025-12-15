using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Services
{
    public interface IPlayerService
    {
        Player CreatePlayer(PlayerCreateDTO playerDto);
        Player? GetPlayerById(int id);
        IEnumerable<Player> GetPlayersByTeam(int teamId);
        Player UpdatePlayer(int id, PlayerUpdateDTO playerDto);
        bool DeletePlayer(int id);
    }
}