using BetTime.Models;
using Microsoft.EntityFrameworkCore;
namespace BetTime.Data;


public class PlayerEFRepository : IPlayerRepository
{
    private readonly BetTimeContext _context;

    public PlayerEFRepository(BetTimeContext context)
    {
        _context = context;
    }

    public Player? GetPlayerById(int id)
        => _context.Players.FirstOrDefault(p => p.Id == id);

    public IEnumerable<Player> GetPlayerByTeam(int teamId)
        => _context.Players.Where(p => p.TeamId == teamId).ToList();

    
    public void AddPlayer(Player player)
    {
        _context.Players.Add(player);
       SaveChanges();
    }

    public void UpdatePlayer(Player player)
    {
        _context.Entry(player).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeletePlayer(Player playerDelete)
    {
        var player = GetPlayerById(playerDelete.Id);
        if (player != null)
            _context.Players.Remove(player);
        SaveChanges();
    }

    public void SaveChanges()
    {
    _context.SaveChanges();    
    }

}
