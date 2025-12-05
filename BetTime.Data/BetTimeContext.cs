using Microsoft.EntityFrameworkCore;
using BetTime.Models;
namespace BetTime.Data;

public class BetTimeContext : DbContext
{
    public BetTimeContext(DbContextOptions<BetTimeContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Bet> Bets { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Sport> Sports { get; set; }
}