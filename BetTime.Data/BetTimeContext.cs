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
        public DbSet<PlayerMarket> PlayerMarkets { get; set; }
        public DbSet<PlayerMarketSelection> PlayerMarketSelections { get; set; }
        public DbSet<MarketSelection> MarketSelections { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerMatchStats> PlayerMatchStats { get; set; }

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
            modelBuilder.Entity<Player>().HasKey(p => p.Id);
            modelBuilder.Entity<PlayerMatchStats>().HasKey(s => s.Id);
            modelBuilder.Entity<PlayerMarket>().HasKey(pm => pm.Id);
            modelBuilder.Entity<PlayerMarketSelection>().HasKey(s => s.Id);

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


modelBuilder.Entity<Match>()
    .HasMany(m => m.Markets)
    .WithOne(market => market.Match)
    .HasForeignKey(market => market.MatchId)
    .OnDelete(DeleteBehavior.Cascade);


modelBuilder.Entity<Market>()
    .HasMany(m => m.Selections)
    .WithOne(s => s.Market)
    .HasForeignKey(s => s.MarketId)
    .OnDelete(DeleteBehavior.Cascade);


    modelBuilder.Entity<Match>()
    .HasMany(m => m.Bets)
    .WithOne(b => b.Match)
    .HasForeignKey(b => b.MatchId)
    .OnDelete(DeleteBehavior.NoAction); 

     modelBuilder.Entity<Player>()
                .HasMany(p => p.MatchStats)
                .WithOne(s => s.Player)
                .HasForeignKey(s => s.PlayerId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<PlayerMatchStats>()
                .HasOne(s => s.Player)
                .WithMany(p => p.MatchStats)
                .HasForeignKey(s => s.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlayerMatchStats>()
                .HasOne<Match>()
                .WithMany(m => m.PlayerMatchStats)
                .HasForeignKey(s => s.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlayerMarket>()
                .HasOne(pm => pm.Player)
                .WithMany(p => p.PlayerMarkets)
                .HasForeignKey(pm => pm.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

           modelBuilder.Entity<PlayerMarket>()
            .HasOne(pm => pm.Match)     
            .WithMany(m => m.PlayerMarkets)
            .HasForeignKey(pm => pm.MatchId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlayerMarket>()
                .HasMany(pm => pm.Selections)
                .WithOne(s => s.PlayerMarket)
                .HasForeignKey(s => s.PlayerMarketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerMarketSelection>()
                .HasOne(s => s.PlayerMarket)
                .WithMany(pm => pm.Selections)
                .HasForeignKey(s => s.PlayerMarketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bet>()
                .HasOne(b => b.PlayerMarketSelection)
                .WithMany() 
                .HasForeignKey(b => b.PlayerMarketSelectionId)
                .OnDelete(DeleteBehavior.Restrict);


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

            modelBuilder.Entity<Player>().HasData(
    
    new Player { Id = 1, Name = "Courtois", TeamId = 1, IsActive = true },
    new Player { Id = 2, Name = "Carvajal", TeamId = 1, IsActive = true },
    new Player { Id = 3, Name = "Militao", TeamId = 1, IsActive = true },
    new Player { Id = 4, Name = "Alaba", TeamId = 1, IsActive = true },
    new Player { Id = 5, Name = "Mendy", TeamId = 1, IsActive = true },
    new Player { Id = 6, Name = "Modric", TeamId = 1, IsActive = true },
    new Player { Id = 7, Name = "Casemiro", TeamId = 1, IsActive = true },
    new Player { Id = 8, Name = "Kroos", TeamId = 1, IsActive = true },
    new Player { Id = 9, Name = "Vinicius", TeamId = 1, IsActive = true },
    new Player { Id = 10, Name = "Benzema", TeamId = 1, IsActive = true },
    new Player { Id = 11, Name = "Rodrygo", TeamId = 1, IsActive = true },

   
    new Player { Id = 12, Name = "Ter Stegen", TeamId = 2, IsActive = true },
    new Player { Id = 13, Name = "Dest", TeamId = 2, IsActive = true },
    new Player { Id = 14, Name = "Piqué", TeamId = 2, IsActive = true },
    new Player { Id = 15, Name = "Araujo", TeamId = 2, IsActive = true },
    new Player { Id = 16, Name = "Alba", TeamId = 2, IsActive = true },
    new Player { Id = 17, Name = "Busquets", TeamId = 2, IsActive = true },
    new Player { Id = 18, Name = "Pedri", TeamId = 2, IsActive = true },
    new Player { Id = 19, Name = "Gavi", TeamId = 2, IsActive = true },
    new Player { Id = 20, Name = "Lewandowski", TeamId = 2, IsActive = true },
    new Player { Id = 21, Name = "Ferran Torres", TeamId = 2, IsActive = true },
    new Player { Id = 22, Name = "Raphinha", TeamId = 2, IsActive = true },

  
    new Player { Id = 23, Name = "Oblak", TeamId = 3, IsActive = true },
    new Player { Id = 24, Name = "Trippier", TeamId = 3, IsActive = true },
    new Player { Id = 25, Name = "Giménez", TeamId = 3, IsActive = true },
    new Player { Id = 26, Name = "Savic", TeamId = 3, IsActive = true },
    new Player { Id = 27, Name = "Reinildo", TeamId = 3, IsActive = true },
    new Player { Id = 28, Name = "Koke", TeamId = 3, IsActive = true },
    new Player { Id = 29, Name = "De Paul", TeamId = 3, IsActive = true },
    new Player { Id = 30, Name = "Saul", TeamId = 3, IsActive = true },
    new Player { Id = 31, Name = "Griezmann", TeamId = 3, IsActive = true },
    new Player { Id = 32, Name = "Correa", TeamId = 3, IsActive = true },
    new Player { Id = 33, Name = "Felix", TeamId = 3, IsActive = true },

   
    new Player { Id = 34, Name = "Bono", TeamId = 4, IsActive = true },
    new Player { Id = 35, Name = "Navas", TeamId = 4, IsActive = true },
    new Player { Id = 36, Name = "Koundé", TeamId = 4, IsActive = true },
    new Player { Id = 37, Name = "Diego Carlos", TeamId = 4, IsActive = true },
    new Player { Id = 38, Name = "Acuna", TeamId = 4, IsActive = true },
    new Player { Id = 39, Name = "Fernando", TeamId = 4, IsActive = true },
    new Player { Id = 40, Name = "Rakitic", TeamId = 4, IsActive = true },
    new Player { Id = 41, Name = "Joan Jordán", TeamId = 4, IsActive = true },
    new Player { Id = 42, Name = "En-Nesyri", TeamId = 4, IsActive = true },
    new Player { Id = 43, Name = "Ocampos", TeamId = 4, IsActive = true },
    new Player { Id = 44, Name = "Martínez", TeamId = 4, IsActive = true },

   
    new Player { Id = 45, Name = "Cillessen", TeamId = 5, IsActive = true },
    new Player { Id = 46, Name = "Gaya", TeamId = 5, IsActive = true },
    new Player { Id = 47, Name = "Alderete", TeamId = 5, IsActive = true },
    new Player { Id = 48, Name = "Diakhaby", TeamId = 5, IsActive = true },
    new Player { Id = 49, Name = "Foulquier", TeamId = 5, IsActive = true },
    new Player { Id = 50, Name = "Soler", TeamId = 5, IsActive = true },
    new Player { Id = 51, Name = "Cáceres", TeamId = 5, IsActive = true },
    new Player { Id = 52, Name = "Guedes", TeamId = 5, IsActive = true },
    new Player { Id = 53, Name = "D. Almeida", TeamId = 5, IsActive = true },
    new Player { Id = 54, Name = "Moreno", TeamId = 5, IsActive = true },
    new Player { Id = 55, Name = "Cheryshev", TeamId = 5, IsActive = true }

    
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