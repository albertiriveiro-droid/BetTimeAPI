using Microsoft.EntityFrameworkCore;
using BetTime.Models;

namespace BetTime.Data
{
    public class BetTimeContext : DbContext
    {
        public BetTimeContext(DbContextOptions<BetTimeContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<MarketSelection> MarketSelections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Claves primarias
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Sport>().HasKey(s => s.Id);
            modelBuilder.Entity<League>().HasKey(l => l.Id);
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Match>().HasKey(m => m.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Bet>().HasKey(b => b.Id);
            modelBuilder.Entity<Market>().HasKey(m => m.Id);
            modelBuilder.Entity<MarketSelection>().HasKey(s => s.Id);

            // Relaciones
           modelBuilder.Entity<User>()
    .HasMany(u => u.Transactions)
    .WithOne(t => t.User)
    .HasForeignKey(t => t.UserId)
    .OnDelete(DeleteBehavior.Cascade);

modelBuilder.Entity<User>()
    .HasMany(u => u.Bets)
    .WithOne(b => b.User)
    .HasForeignKey(b => b.UserId)
    .OnDelete(DeleteBehavior.Cascade);

// Deportes y ligas
modelBuilder.Entity<Sport>()
    .HasMany(s => s.Leagues)
    .WithOne(l => l.Sport)
    .HasForeignKey(l => l.SportId)
    .OnDelete(DeleteBehavior.Cascade);

modelBuilder.Entity<League>()
    .HasMany(l => l.Teams)
    .WithOne(t => t.League)
    .HasForeignKey(t => t.LeagueId)
    .OnDelete(DeleteBehavior.Cascade);

modelBuilder.Entity<League>()
    .HasMany(l => l.Matches)
    .WithOne(m => m.League)
    .HasForeignKey(m => m.LeagueId)
    .OnDelete(DeleteBehavior.Cascade);

// Equipos y partidos
modelBuilder.Entity<Team>()
    .HasMany(t => t.HomeMatches)
    .WithOne(m => m.HomeTeam)
    .HasForeignKey(m => m.HomeTeamId)
    .OnDelete(DeleteBehavior.Restrict);

modelBuilder.Entity<Team>()
    .HasMany(t => t.AwayMatches)
    .WithOne(m => m.AwayTeam)
    .HasForeignKey(m => m.AwayTeamId)
    .OnDelete(DeleteBehavior.Restrict);

// Partidos y mercados
modelBuilder.Entity<Match>()
    .HasMany(m => m.Markets)
    .WithOne(market => market.Match)
    .HasForeignKey(market => market.MatchId)
    .OnDelete(DeleteBehavior.Cascade);

// Mercados y selecciones
modelBuilder.Entity<Market>()
    .HasMany(m => m.Selections)
    .WithOne(s => s.Market)
    .HasForeignKey(s => s.MarketId)
    .OnDelete(DeleteBehavior.Cascade);

// Partidos y apuestas
    modelBuilder.Entity<Match>()
    .HasMany(m => m.Bets)
    .WithOne(b => b.Match)
    .HasForeignKey(b => b.MatchId)
    .OnDelete(DeleteBehavior.NoAction); 

            // Seed de datos
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username= "juan123", Email = "juan@example.com", Password = "pass123", Balance = 100, Role = Roles.User },
                new User { Id = 2, Username= "albertorbd", Email = "albertoriveiro@hotmail.es", Password = "pass456", Balance = 150, Role = Roles.Admin },
                new User { Id = 3, Username="laura234", Email = "laura@example.com", Password = "pass789", Balance = 120, Role = Roles.User }
            );

            modelBuilder.Entity<Sport>().HasData(
                new Sport { Id = 1, Name = "Fútbol" }
            );

            modelBuilder.Entity<League>().HasData(
                new League { Id = 1, Name = "LaLiga", SportId = 1 },
                new League { Id = 2, Name = "Bundesliga", SportId = 1 },
                new League { Id = 3, Name = "Premier League", SportId = 1 }
            );

            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Real Madrid", LeagueId = 1 },
                new Team { Id = 2, Name = "Barcelona", LeagueId = 1 },
                new Team { Id = 3, Name = "Atlético Madrid", LeagueId = 1 },
                new Team { Id = 4, Name = "Sevilla", LeagueId = 1 },
                new Team { Id = 5, Name = "Valencia", LeagueId = 1 },

                new Team { Id = 6, Name = "Bayern Munich", LeagueId = 2 },
                new Team { Id = 7, Name = "Borussia Dortmund", LeagueId = 2 },
                new Team { Id = 8, Name = "RB Leipzig", LeagueId = 2 },
                new Team { Id = 9, Name = "Bayer Leverkusen", LeagueId = 2 },
                new Team { Id = 10, Name = "Schalke 04", LeagueId = 2 },

                new Team { Id = 11, Name = "Manchester United", LeagueId = 3 },
                new Team { Id = 12, Name = "Liverpool", LeagueId = 3 },
                new Team { Id = 13, Name = "Chelsea", LeagueId = 3 },
                new Team { Id = 14, Name = "Arsenal", LeagueId = 3 },
                new Team { Id = 15, Name = "Manchester City", LeagueId = 3 }
            );

            modelBuilder.Entity<Match>().HasData(
                new Match { Id = 1, LeagueId = 1, HomeTeamId = 1, AwayTeamId = 2, StartTime = new DateTime(2025, 12, 11, 21, 0, 0),  DurationMinutes = 90, Finished = false },
                new Match { Id = 2, LeagueId = 2, HomeTeamId = 6, AwayTeamId = 7, StartTime = new DateTime(2025, 12, 11, 21, 0, 0),  DurationMinutes = 90, Finished = false },
                new Match { Id = 3, LeagueId = 3, HomeTeamId = 11, AwayTeamId = 12, StartTime = new DateTime(2025, 12, 11, 21, 0, 0), DurationMinutes = 90, Finished = false }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, UserId = 1, Amount = 50, Date = DateTime.UtcNow.AddDays(-2), PaymentMethod = "Tarjeta", Type = "DEPOSIT"  },
                new Transaction { Id = 2, UserId = 1, Amount = 25, Date = DateTime.UtcNow.AddDays(-1), PaymentMethod = "PayPal", Type = "DEPOSIT" },

                new Transaction { Id = 3, UserId = 2, Amount = 100, Date = DateTime.UtcNow.AddDays(-3), PaymentMethod = "Tarjeta", Type = "DEPOSIT" },
                new Transaction { Id = 4, UserId = 2, Amount = 50, Date = DateTime.UtcNow.AddDays(-1), PaymentMethod = "PayPal", Type = "WITHDRAW"},

                new Transaction { Id = 5, UserId = 3, Amount = 75, Date = DateTime.UtcNow.AddDays(-2), PaymentMethod = "Tarjeta", Type =  "DEPOSIT" },
                new Transaction { Id = 6, UserId = 3, Amount = 30, Date = DateTime.UtcNow.AddDays(-1), PaymentMethod = "PayPal", Type = "WITHDRAW" }
            );

          modelBuilder.Entity<Market>().HasData(
         new Market { Id = 1, MatchId = 1, MarketType = MarketType.OneXTwo, Description = "Resultado final" },
        new Market { Id = 2, MatchId = 1, MarketType = MarketType.OverUnderGoals, Description = "Más/Menos de 2.5 goles" },

        new Market { Id = 3, MatchId = 2, MarketType = MarketType.OneXTwo, Description = "Resultado final" },
        new Market { Id = 4, MatchId = 2, MarketType = MarketType.OverUnderGoals, Description = "Más/Menos de 2.5 goles" },

        new Market { Id = 5, MatchId = 3, MarketType = MarketType.OneXTwo, Description = "Resultado final" },
        new Market { Id = 6, MatchId = 3, MarketType = MarketType.OverUnderGoals, Description = "Más/Menos de 2.5 goles" }
);

            
        modelBuilder.Entity<MarketSelection>().HasData(
                new MarketSelection { Id = 1, MarketId = 1, Name = "Real Madrid gana", Odd = 1.8m },
                new MarketSelection { Id = 2, MarketId = 1, Name = "Empate", Odd = 3.2m },
                new MarketSelection { Id = 3, MarketId = 1, Name = "Barcelona gana", Odd = 4.0m },

                new MarketSelection { Id = 4, MarketId = 2, Name = "Más de 2.5 goles", Odd = 2.1m },
                new MarketSelection { Id = 5, MarketId = 2, Name = "Menos de 2.5 goles", Odd = 1.7m },

                new MarketSelection { Id = 6, MarketId = 3, Name = "Bayern Munich gana", Odd = 1.5m },
                new MarketSelection { Id = 7, MarketId = 3, Name = "Empate", Odd = 3.5m },
                new MarketSelection { Id = 8, MarketId = 3, Name = "Borussia Dortmund gana", Odd = 5.0m },

                new MarketSelection { Id = 9, MarketId = 4, Name = "Más de 2.5 goles", Odd = 1.9m },
                new MarketSelection { Id = 10, MarketId = 4, Name = "Menos de 2.5 goles", Odd = 2.0m },

                new MarketSelection { Id = 11, MarketId = 5, Name = "Manchester United gana", Odd = 2.0m },
                new MarketSelection { Id = 12, MarketId = 5, Name = "Empate", Odd = 3.0m },
                new MarketSelection { Id = 13, MarketId = 5, Name = "Liverpool gana", Odd = 3.5m },

                new MarketSelection { Id = 14, MarketId = 6, Name = "Más de 2.5 goles", Odd = 2.2m },
                new MarketSelection { Id = 15, MarketId = 6, Name = "Menos de 2.5 goles", Odd = 1.65m }
            );

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }
}