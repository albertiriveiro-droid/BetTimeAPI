using BetTime.Models;
namespace BetTime.Data;


public interface IPlayerRepository
{
    Player? GetPlayerById(int id);
    IEnumerable<Player> GetPlayerByTeam(int teamId);
    void AddPlayer(Player player);
    void UpdatePlayer(Player player);
    void DeletePlayer(Player player);
     void SaveChanges();  
}