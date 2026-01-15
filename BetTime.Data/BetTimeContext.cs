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


           
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username= "juan123", Email = "juan@example.com", Password = "pass123", Balance = 100, Role = Roles.User },
                new User { Id = 2, Username= "admin123", Email = "admin@hotmail.es", Password = "pass456", Balance = 150, Role = Roles.Admin },
                new User { Id = 3, Username="laura234", Email = "laura@example.com", Password = "pass789", Balance = 120, Role = Roles.User }
            );

            modelBuilder.Entity<Sport>().HasData(
                new Sport { Id = 1, Name = "Fútbol" }
            );

            modelBuilder.Entity<League>().HasData(
                new League { Id = 1, Name = "LaLiga", SportId = 1 },
                new League { Id = 2, Name = "Bundesliga", SportId = 1 },
                new League { Id = 3, Name = "Premier League", SportId = 1 },
                new League { Id = 4, Name = "Serie A", SportId = 1 },
                new League { Id = 5, Name = "Ligue 1", SportId = 1 }
            );

            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Real Madrid", LeagueId = 1 },
                new Team { Id = 2, Name = "Barcelona", LeagueId = 1 },
                new Team { Id = 3, Name = "Atlético Madrid", LeagueId = 1 },
                new Team { Id = 4, Name = "Sevilla", LeagueId = 1 },
                new Team { Id = 5, Name = "Valencia", LeagueId = 1 },
                 new Team { Id = 6, Name = "Villarreal", LeagueId = 1 },
                new Team { Id = 7, Name = "RCD Espanyol", LeagueId = 1 },
                new Team { Id = 8, Name = "Real Betis", LeagueId = 1 },
                new Team { Id = 9, Name = "Athletic", LeagueId = 1 },
                new Team { Id = 10, Name = "Celta de Vigo", LeagueId = 1 },
                

                new Team { Id = 11, Name = "Bayern Munich", LeagueId = 2 },
                new Team { Id = 12, Name = "Borussia Dortmund", LeagueId = 2 },
                new Team { Id = 13, Name = "RB Leipzig", LeagueId = 2 },
                new Team { Id = 14, Name = "Bayer Leverkusen", LeagueId = 2 },
                new Team { Id = 15, Name = "Mainz 05", LeagueId = 2 },
                 new Team { Id = 16, Name = "Hoffenheim", LeagueId = 2 },
                new Team { Id = 17, Name = "Stuttgart", LeagueId = 2 },
                new Team { Id = 18, Name = "Frankfurt", LeagueId = 2 },
                new Team { Id = 19, Name = "FC Union Berlin", LeagueId = 2 },
                new Team { Id = 20, Name = "Friburgo", LeagueId = 2 },


                new Team { Id = 21, Name = "Manchester United", LeagueId = 3 },
                new Team { Id = 22, Name = "Liverpool", LeagueId = 3 },
                new Team { Id = 23, Name = "Chelsea", LeagueId = 3 },
                new Team { Id = 24, Name = "Arsenal", LeagueId = 3 },
                new Team { Id = 25, Name = "Manchester City", LeagueId = 3 },
                new Team { Id = 26, Name = "Crystal Palace", LeagueId = 3 },
                new Team { Id = 27, Name = "Sunderland", LeagueId = 3 },
                new Team { Id = 28, Name = "Everton", LeagueId = 3 },
                new Team { Id = 29, Name = "Brighton", LeagueId = 3 },
                new Team { Id = 30, Name = "Tottenham", LeagueId = 3 },
        

                new Team { Id = 31, Name = "Milan", LeagueId = 4 },
                new Team { Id = 32, Name = "Inter", LeagueId = 4 },
                new Team { Id = 33, Name = "Napoli", LeagueId = 4 },
                new Team { Id = 34, Name = "Roma", LeagueId = 4 },
                new Team { Id = 35, Name = "Juventus", LeagueId = 4 },
                new Team { Id = 36, Name = "Bolonia", LeagueId = 4 },
                new Team { Id = 37, Name = "Como", LeagueId = 4 },
                new Team { Id = 38, Name = "Lazio", LeagueId = 4 },
                new Team { Id = 39, Name = "Sassuolo", LeagueId = 4 },
                new Team { Id = 40, Name = "Udinese", LeagueId = 4 },
        

                new Team { Id = 41, Name = "PSG", LeagueId = 5 },
                new Team { Id = 42, Name = "Lens", LeagueId = 5 },
                new Team { Id = 43, Name = "Marsella", LeagueId = 5 },
                new Team { Id = 44, Name = "LOSC", LeagueId = 5 },
                new Team { Id = 45, Name = "Lyon", LeagueId = 5 },
                new Team { Id = 46, Name = "Rennes", LeagueId = 5 },
                new Team { Id = 47, Name = "Racing de Estrasburgo", LeagueId = 5 },
                new Team { Id = 48, Name = "Toulouse", LeagueId = 5 },
                new Team { Id = 49, Name = "Mónaco", LeagueId = 5 },
                new Team { Id = 50, Name = "Angers", LeagueId = 5 }
                         

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
    
    new Player { Id = 1, Name = "Courtois", TeamId = 1, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 2, Name = "Carvajal", TeamId = 1, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 3, Name = "Militao", TeamId = 1, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 4, Name = "Alaba", TeamId = 1, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 5, Name = "Mendy", TeamId = 1, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 6, Name = "Tchouameni", TeamId = 1, ShirtNumber = 18, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 7, Name = "Valverde", TeamId = 1, ShirtNumber = 15, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 8, Name = "Bellingham", TeamId = 1, ShirtNumber = 5, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 9, Name = "Rodrygo", TeamId = 1, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 10, Name = "Mbappe", TeamId = 1, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 11, Name = "Vinicius", TeamId = 1, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 12, Name = "Ter Stegen", TeamId = 2, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 13, Name = "Kounde", TeamId = 2, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 14, Name = "Araujo", TeamId = 2, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 15, Name = "Christensen", TeamId = 2, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 16, Name = "Balde", TeamId = 2, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 17, Name = "De Jong", TeamId = 2, ShirtNumber = 21, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 18, Name = "Pedri", TeamId = 2, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 19, Name = "Gavi", TeamId = 2, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 20, Name = "Yamal", TeamId = 2, ShirtNumber = 27, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 21, Name = "Lewandowski", TeamId = 2, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 22, Name = "Raphinha", TeamId = 2, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
  
    new Player { Id = 23, Name = "Oblak", TeamId = 3, ShirtNumber = 13, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 24, Name = "Molina", TeamId = 3, ShirtNumber = 16, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 25, Name = "Gimenez", TeamId = 3, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 26, Name = "Savic", TeamId = 3, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 27, Name = "Reinildo", TeamId = 3, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 28, Name = "Koke", TeamId = 3, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 29, Name = "De Paul", TeamId = 3, ShirtNumber = 5, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 30, Name = "Saul", TeamId = 3, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 31, Name = "Griezmann", TeamId = 3, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 32, Name = "Morata", TeamId = 3, ShirtNumber = 19, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 33, Name = "Correa", TeamId = 3, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
   
    new Player { Id = 34, Name = "Nyland", TeamId = 4, ShirtNumber = 13, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 35, Name = "Navas", TeamId = 4, ShirtNumber = 16, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 36, Name = "Ramos", TeamId = 4, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 37, Name = "Bade", TeamId = 4, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 38, Name = "Acuna", TeamId = 4, ShirtNumber = 19, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 39, Name = "Soumare", TeamId = 4, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 40, Name = "Rakitic", TeamId = 4, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 41, Name = "Ocampos", TeamId = 4, ShirtNumber = 5, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 42, Name = "Suso", TeamId = 4, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 43, Name = "En-Nesyri", TeamId = 4, ShirtNumber = 15, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 44, Name = "Lamela", TeamId = 4, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },

   
    new Player { Id = 45, Name = "Mamardashvili", TeamId = 5, ShirtNumber = 25, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 46, Name = "Correia", TeamId = 5, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 47, Name = "Mosquera", TeamId = 5, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 48, Name = "Diakhaby", TeamId = 5, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 49, Name = "Gaya", TeamId = 5, ShirtNumber = 14, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 50, Name = "Pepelu", TeamId = 5, ShirtNumber = 18, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 51, Name = "Javi Guerra", TeamId = 5, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 52, Name = "Almeida", TeamId = 5, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 53, Name = "Diego Lopez", TeamId = 5, ShirtNumber = 16, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 54, Name = "Hugo Duro", TeamId = 5, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 55, Name = "Canos", TeamId = 5, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },


    new Player { Id = 56, Name = "Jorgensen", TeamId = 6, ShirtNumber = 13, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 57, Name = "Foyth", TeamId = 6, ShirtNumber = 8, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 58, Name = "Albiol", TeamId = 6, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 59, Name = "Cuenca", TeamId = 6, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 60, Name = "Pedraza", TeamId = 6, ShirtNumber = 24, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 61, Name = "Parejo", TeamId = 6, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 62, Name = "Capoue", TeamId = 6, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 63, Name = "Baena", TeamId = 6, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 64, Name = "Guedes", TeamId = 6, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 65, Name = "Gerard Moreno", TeamId = 6, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 66, Name = "Sorloth", TeamId = 6, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 67, Name = "Pacheco", TeamId = 7, ShirtNumber = 13, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 68, Name = "Oscar Gil", TeamId = 7, ShirtNumber = 20, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 69, Name = "Cabrera", TeamId = 7, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 70, Name = "Calero", TeamId = 7, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 71, Name = "Brian Olivan", TeamId = 7, ShirtNumber = 14, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 72, Name = "Expósito", TeamId = 7, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 73, Name = "Gragera", TeamId = 7, ShirtNumber = 15, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 74, Name = "Melamed", TeamId = 7, ShirtNumber = 21, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 75, Name = "Puado", TeamId = 7, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 76, Name = "Braithwaite", TeamId = 7, ShirtNumber = 22, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 77, Name = "Jofre", TeamId = 7, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 78, Name = "Rui Silva", TeamId = 8, ShirtNumber = 13, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 79, Name = "Bellerin", TeamId = 8, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 80, Name = "Pezzella", TeamId = 8, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 81, Name = "Bartra", TeamId = 8, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 82, Name = "Miranda", TeamId = 8, ShirtNumber = 33, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 83, Name = "Guido Rodriguez", TeamId = 8, ShirtNumber = 4, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 84, Name = "Isco", TeamId = 8, ShirtNumber = 22, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 85, Name = "Fekir", TeamId = 8, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 86, Name = "Ayoze", TeamId = 8, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 87, Name = "Borja Iglesias", TeamId = 8, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 88, Name = "Luiz Henrique", TeamId = 8, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 89, Name = "Unai Simon", TeamId = 9, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 90, Name = "De Marcos", TeamId = 9, ShirtNumber = 18, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 91, Name = "Vivian", TeamId = 9, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 92, Name = "Yeray", TeamId = 9, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 93, Name = "Yuri", TeamId = 9, ShirtNumber = 17, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 94, Name = "Vesga", TeamId = 9, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 95, Name = "Sancet", TeamId = 9, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 96, Name = "Muniain", TeamId = 9, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 97, Name = "Nico Williams", TeamId = 9, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 98, Name = "Guruzeta", TeamId = 9, ShirtNumber = 12, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 99, Name = "Inaki Williams", TeamId = 9, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    
    new Player { Id = 100, Name = "Guaita", TeamId = 10, ShirtNumber = 13, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 101, Name = "Mallo", TeamId = 10, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 102, Name = "Starfelt", TeamId = 10, ShirtNumber = 24, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 103, Name = "Aidoo", TeamId = 10, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 104, Name = "Mingueza", TeamId = 10, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 105, Name = "Beltran", TeamId = 10, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 106, Name = "De la Torre", TeamId = 10, ShirtNumber = 14, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 107, Name = "Bamba", TeamId = 10, ShirtNumber = 17, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 108, Name = "Aspas", TeamId = 10, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 109, Name = "Strand Larsen", TeamId = 10, ShirtNumber = 18, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 110, Name = "Douvikas", TeamId = 10, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 111, Name = "Neuer", TeamId = 11, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 112, Name = "Kimmich", TeamId = 11, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 113, Name = "De Ligt", TeamId = 11, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 114, Name = "Upamecano", TeamId = 11, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 115, Name = "Davies", TeamId = 11, ShirtNumber = 19, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 116, Name = "Goretzka", TeamId = 11, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 117, Name = "Laimer", TeamId = 11, ShirtNumber = 27, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 118, Name = "Musiala", TeamId = 11, ShirtNumber = 42, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 119, Name = "Sane", TeamId = 11, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 120, Name = "Kane", TeamId = 11, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 121, Name = "Coman", TeamId = 11, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 122, Name = "Kobel", TeamId = 12, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 123, Name = "Ryerson", TeamId = 12, ShirtNumber = 26, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 124, Name = "Hummels", TeamId = 12, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 125, Name = "Schlotterbeck", TeamId = 12, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 126, Name = "Bensebaini", TeamId = 12, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 127, Name = "Can", TeamId = 12, ShirtNumber = 23, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 128, Name = "Brandt", TeamId = 12, ShirtNumber = 19, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 129, Name = "Sabitzer", TeamId = 12, ShirtNumber = 20, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 130, Name = "Adeyemi", TeamId = 12, ShirtNumber = 27, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 131, Name = "Füllkrug", TeamId = 12, ShirtNumber = 14, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 132, Name = "Malen", TeamId = 12, ShirtNumber = 21, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 133, Name = "Gulacsi", TeamId = 13, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 134, Name = "Henrichs", TeamId = 13, ShirtNumber = 39, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 135, Name = "Orban", TeamId = 13, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 136, Name = "Simakan", TeamId = 13, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 137, Name = "Raum", TeamId = 13, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 138, Name = "Schlager", TeamId = 13, ShirtNumber = 24, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 139, Name = "Haidara", TeamId = 13, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 140, Name = "Olmo", TeamId = 13, ShirtNumber = 7, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 141, Name = "Openda", TeamId = 13, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 142, Name = "Sesko", TeamId = 13, ShirtNumber = 30, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 143, Name = "Simons", TeamId = 13, ShirtNumber = 20, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 144, Name = "Hradecky", TeamId = 14, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 145, Name = "Frimpong", TeamId = 14, ShirtNumber = 30, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 146, Name = "Tah", TeamId = 14, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 147, Name = "Tapsoba", TeamId = 14, ShirtNumber = 12, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 148, Name = "Grimaldo", TeamId = 14, ShirtNumber = 20, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 149, Name = "Xhaka", TeamId = 14, ShirtNumber = 34, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 150, Name = "Palacios", TeamId = 14, ShirtNumber = 25, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 151, Name = "Wirtz", TeamId = 14, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 152, Name = "Adli", TeamId = 14, ShirtNumber = 21, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 153, Name = "Schick", TeamId = 14, ShirtNumber = 14, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 154, Name = "Boniface", TeamId = 14, ShirtNumber = 22, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 155, Name = "Zentner", TeamId = 15, ShirtNumber = 27, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 156, Name = "Widmer", TeamId = 15, ShirtNumber = 30, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 157, Name = "Bell", TeamId = 15, ShirtNumber = 16, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 158, Name = "Hanche-Olsen", TeamId = 15, ShirtNumber = 25, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 159, Name = "Caci", TeamId = 15, ShirtNumber = 19, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 160, Name = "Kohr", TeamId = 15, ShirtNumber = 31, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 161, Name = "Barreiro", TeamId = 15, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 162, Name = "Lee Jae-Sung", TeamId = 15, ShirtNumber = 7, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 163, Name = "Burkardt", TeamId = 15, ShirtNumber = 29, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 164, Name = "Onisiwo", TeamId = 15, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 165, Name = "Ajorque", TeamId = 15, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 166, Name = "Baumann", TeamId = 16, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 167, Name = "Kaderabek", TeamId = 16, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 168, Name = "Akpoguma", TeamId = 16, ShirtNumber = 25, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 169, Name = "Vogt", TeamId = 16, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 170, Name = "Skov", TeamId = 16, ShirtNumber = 29, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 171, Name = "Grillitsch", TeamId = 16, ShirtNumber = 11, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 172, Name = "Stach", TeamId = 16, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 173, Name = "Kramaric", TeamId = 16, ShirtNumber = 27, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 174, Name = "Bebou", TeamId = 16, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 175, Name = "Beier", TeamId = 16, ShirtNumber = 14, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 176, Name = "Weghorst", TeamId = 16, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 177, Name = "Nubel", TeamId = 17, ShirtNumber = 33, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 178, Name = "Vagnoman", TeamId = 17, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 179, Name = "Anton", TeamId = 17, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 180, Name = "Ito", TeamId = 17, ShirtNumber = 21, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 181, Name = "Mittelstadt", TeamId = 17, ShirtNumber = 7, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 182, Name = "Karazor", TeamId = 17, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 183, Name = "Stiller", TeamId = 17, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 184, Name = "Millot", TeamId = 17, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 185, Name = "Führich", TeamId = 17, ShirtNumber = 27, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 186, Name = "Guirassy", TeamId = 17, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 187, Name = "Undav", TeamId = 17, ShirtNumber = 26, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 188, Name = "Trapp", TeamId = 18, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 189, Name = "Tuta", TeamId = 18, ShirtNumber = 35, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 190, Name = "Koch", TeamId = 18, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 191, Name = "Pacho", TeamId = 18, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 192, Name = "Nkounkou", TeamId = 18, ShirtNumber = 29, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 193, Name = "Skhiri", TeamId = 18, ShirtNumber = 15, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 194, Name = "Larsson", TeamId = 18, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 195, Name = "Gotze", TeamId = 18, ShirtNumber = 27, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 196, Name = "Knauff", TeamId = 18, ShirtNumber = 36, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 197, Name = "Ekitike", TeamId = 18, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 198, Name = "Marmoush", TeamId = 18, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 199, Name = "Ronnnow", TeamId = 19, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 200, Name = "Juranovic", TeamId = 19, ShirtNumber = 18, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 201, Name = "Knoche", TeamId = 19, ShirtNumber = 31, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 202, Name = "Doekhi", TeamId = 19, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 203, Name = "Leite", TeamId = 19, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 204, Name = "Khedira", TeamId = 19, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 205, Name = "Laidouni", TeamId = 19, ShirtNumber = 20, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 206, Name = "Haberer", TeamId = 19, ShirtNumber = 19, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 207, Name = "Behrens", TeamId = 19, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 208, Name = "Volland", TeamId = 19, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 209, Name = "Jordan", TeamId = 19, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 210, Name = "Atubolu", TeamId = 20, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 211, Name = "Kübler", TeamId = 20, ShirtNumber = 17, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 212, Name = "Ginter", TeamId = 20, ShirtNumber = 28, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 213, Name = "Lienhart", TeamId = 20, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 214, Name = "Günther", TeamId = 20, ShirtNumber = 30, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 215, Name = "Höfler", TeamId = 20, ShirtNumber = 27, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 216, Name = "Eggestein", TeamId = 20, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 217, Name = "Doan", TeamId = 20, ShirtNumber = 42, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 218, Name = "Grifo", TeamId = 20, ShirtNumber = 32, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 219, Name = "Gregoritsch", TeamId = 20, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 220, Name = "Sallai", TeamId = 20, ShirtNumber = 22, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 221, Name = "Onana", TeamId = 21, ShirtNumber = 24, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 222, Name = "Dalot", TeamId = 21, ShirtNumber = 20, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 223, Name = "Varane", TeamId = 21, ShirtNumber = 19, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 224, Name = "Martinez", TeamId = 21, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 225, Name = "Shaw", TeamId = 21, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 226, Name = "Casemiro", TeamId = 21, ShirtNumber = 18, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 227, Name = "Bruno Fernandes", TeamId = 21, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 228, Name = "Mount", TeamId = 21, ShirtNumber = 7, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 229, Name = "Rashford", TeamId = 21, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 230, Name = "Hojlund", TeamId = 21, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 231, Name = "Garnacho", TeamId = 21, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 232, Name = "Alisson", TeamId = 22, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 233, Name = "Alexander-Arnold", TeamId = 22, ShirtNumber = 66, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 234, Name = "Van Dijk", TeamId = 22, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 235, Name = "Konate", TeamId = 22, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 236, Name = "Robertson", TeamId = 22, ShirtNumber = 26, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 237, Name = "Mac Allister", TeamId = 22, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 238, Name = "Szoboszlai", TeamId = 22, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 239, Name = "Gravenberch", TeamId = 22, ShirtNumber = 38, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 240, Name = "Salah", TeamId = 22, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 241, Name = "Nunez", TeamId = 22, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 242, Name = "Diaz", TeamId = 22, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 243, Name = "Petrovic", TeamId = 23, ShirtNumber = 28, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 244, Name = "James", TeamId = 23, ShirtNumber = 24, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 245, Name = "Disasi", TeamId = 23, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 246, Name = "Colwill", TeamId = 23, ShirtNumber = 26, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 247, Name = "Chilwell", TeamId = 23, ShirtNumber = 21, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 248, Name = "Caicedo", TeamId = 23, ShirtNumber = 25, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 249, Name = "Enzo Fernandez", TeamId = 23, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 250, Name = "Palmer", TeamId = 23, ShirtNumber = 20, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 251, Name = "Sterling", TeamId = 23, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 252, Name = "Jackson", TeamId = 23, ShirtNumber = 15, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 253, Name = "Mudryk", TeamId = 23, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 254, Name = "Raya", TeamId = 24, ShirtNumber = 22, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 255, Name = "White", TeamId = 24, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 256, Name = "Saliba", TeamId = 24, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 257, Name = "Gabriel", TeamId = 24, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 258, Name = "Zinchenko", TeamId = 24, ShirtNumber = 35, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 259, Name = "Rice", TeamId = 24, ShirtNumber = 41, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 260, Name = "Odegaard", TeamId = 24, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 261, Name = "Havertz", TeamId = 24, ShirtNumber = 29, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 262, Name = "Saka", TeamId = 24, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 263, Name = "Jesus", TeamId = 24, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 264, Name = "Martinelli", TeamId = 24, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 265, Name = "Ederson", TeamId = 25, ShirtNumber = 31, Position = PlayerPosition.Goalkeeper, IsActive = true },

    new Player { Id = 266, Name = "Walker", TeamId = 25, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 267, Name = "Dias", TeamId = 25, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 268, Name = "Stones", TeamId = 25, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
    new Player { Id = 269, Name = "Gvardiol", TeamId = 25, ShirtNumber = 24, Position = PlayerPosition.Defender, IsActive = true },

    new Player { Id = 270, Name = "Rodri", TeamId = 25, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 271, Name = "De Bruyne", TeamId = 25, ShirtNumber = 17, Position = PlayerPosition.Midfielder, IsActive = true },
    new Player { Id = 272, Name = "Foden", TeamId = 25, ShirtNumber = 47, Position = PlayerPosition.Midfielder, IsActive = true },

    new Player { Id = 273, Name = "Silva", TeamId = 25, ShirtNumber = 20, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 274, Name = "Haaland", TeamId = 25, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
    new Player { Id = 275, Name = "Doku", TeamId = 25, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },

    new Player { Id = 276, Name = "Johnstone", TeamId = 26, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 277, Name = "Clyne", TeamId = 26, ShirtNumber = 17, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 278, Name = "Guehi", TeamId = 26, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 279, Name = "Andersen", TeamId = 26, ShirtNumber = 16, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 280, Name = "Mitchell", TeamId = 26, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 281, Name = "Doucoure", TeamId = 26, ShirtNumber = 28, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 282, Name = "Lerma", TeamId = 26, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 283, Name = "Eze", TeamId = 26, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 284, Name = "Olise", TeamId = 26, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 285, Name = "Mateta", TeamId = 26, ShirtNumber = 14, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 286, Name = "Ayew", TeamId = 26, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 287, Name = "Patterson", TeamId = 27, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 288, Name = "Hume", TeamId = 27, ShirtNumber = 32, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 289, Name = "Ballard", TeamId = 27, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 290, Name = "O'Nien", TeamId = 27, ShirtNumber = 13, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 291, Name = "Cirkin", TeamId = 27, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 292, Name = "Neil", TeamId = 27, ShirtNumber = 24, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 293, Name = "Ekwah", TeamId = 27, ShirtNumber = 14, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 294, Name = "Bellingham", TeamId = 27, ShirtNumber = 7, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 295, Name = "Roberts", TeamId = 27, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 296, Name = "Clarke", TeamId = 27, ShirtNumber = 20, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 297, Name = "Burstow", TeamId = 27, ShirtNumber = 19, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 298, Name = "Pickford", TeamId = 28, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 299, Name = "Coleman", TeamId = 28, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 300, Name = "Tarkowski", TeamId = 28, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 301, Name = "Branthwaite", TeamId = 28, ShirtNumber = 32, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 302, Name = "Mykolenko", TeamId = 28, ShirtNumber = 19, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 303, Name = "Gueye", TeamId = 28, ShirtNumber = 27, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 304, Name = "Onana", TeamId = 28, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 305, Name = "Doucoure", TeamId = 28, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 306, Name = "McNeil", TeamId = 28, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 307, Name = "Calvert-Lewin", TeamId = 28, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 308, Name = "Harrison", TeamId = 28, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 309, Name = "Verbruggen", TeamId = 29, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 310, Name = "Lamptey", TeamId = 29, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 311, Name = "Dunk", TeamId = 29, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 312, Name = "Van Hecke", TeamId = 29, ShirtNumber = 29, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 313, Name = "Estupiñan", TeamId = 29, ShirtNumber = 30, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 314, Name = "Gross", TeamId = 29, ShirtNumber = 13, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 315, Name = "Gilmour", TeamId = 29, ShirtNumber = 11, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 316, Name = "Enciso", TeamId = 29, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 317, Name = "Mitoma", TeamId = 29, ShirtNumber = 22, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 318, Name = "Pedro", TeamId = 29, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 319, Name = "Adingra", TeamId = 29, ShirtNumber = 24, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 320, Name = "Vicario", TeamId = 30, ShirtNumber = 13, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 321, Name = "Porro", TeamId = 30, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 322, Name = "Romero", TeamId = 30, ShirtNumber = 17, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 323, Name = "Van de Ven", TeamId = 30, ShirtNumber = 37, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 324, Name = "Udogie", TeamId = 30, ShirtNumber = 38, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 325, Name = "Bissouma", TeamId = 30, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 326, Name = "Bentancur", TeamId = 30, ShirtNumber = 30, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 327, Name = "Maddison", TeamId = 30, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 328, Name = "Kulusevski", TeamId = 30, ShirtNumber = 21, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 329, Name = "Son", TeamId = 30, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 330, Name = "Richarlison", TeamId = 30, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 331, Name = "Maignan", TeamId = 31, ShirtNumber = 16, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 332, Name = "Calabria", TeamId = 31, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 333, Name = "Tomori", TeamId = 31, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 334, Name = "Thiaw", TeamId = 31, ShirtNumber = 28, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 335, Name = "Theo Hernandez", TeamId = 31, ShirtNumber = 19, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 336, Name = "Bennacer", TeamId = 31, ShirtNumber = 4, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 337, Name = "Reijnders", TeamId = 31, ShirtNumber = 14, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 338, Name = "Pulisic", TeamId = 31, ShirtNumber = 11, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 339, Name = "Leao", TeamId = 31, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 340, Name = "Giroud", TeamId = 31, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 341, Name = "Chukwueze", TeamId = 31, ShirtNumber = 21, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 342, Name = "Sommer", TeamId = 32, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 343, Name = "Pavard", TeamId = 32, ShirtNumber = 28, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 344, Name = "Acerbi", TeamId = 32, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 345, Name = "Bastoni", TeamId = 32, ShirtNumber = 95, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 346, Name = "Darmian", TeamId = 32, ShirtNumber = 36, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 347, Name = "Calhanoglu", TeamId = 32, ShirtNumber = 20, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 348, Name = "Barella", TeamId = 32, ShirtNumber = 23, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 349, Name = "Mkhitaryan", TeamId = 32, ShirtNumber = 22, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 350, Name = "Thuram", TeamId = 32, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 351, Name = "Lautaro Martinez", TeamId = 32, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 352, Name = "Dimarco", TeamId = 32, ShirtNumber = 32, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 353, Name = "Meret", TeamId = 33, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 354, Name = "Di Lorenzo", TeamId = 33, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 355, Name = "Rrahmani", TeamId = 33, ShirtNumber = 13, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 356, Name = "Juan Jesus", TeamId = 33, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 357, Name = "Olivera", TeamId = 33, ShirtNumber = 17, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 358, Name = "Lobotka", TeamId = 33, ShirtNumber = 68, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 359, Name = "Anguissa", TeamId = 33, ShirtNumber = 99, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 360, Name = "Zielinski", TeamId = 33, ShirtNumber = 20, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 361, Name = "Politano", TeamId = 33, ShirtNumber = 21, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 362, Name = "Osimhen", TeamId = 33, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 363, Name = "Kvaratskhelia", TeamId = 33, ShirtNumber = 77, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 364, Name = "Svilar", TeamId = 34, ShirtNumber = 99, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 365, Name = "Mancini", TeamId = 34, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 366, Name = "Smalling", TeamId = 34, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 367, Name = "Ndicka", TeamId = 34, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 368, Name = "Spinazzola", TeamId = 34, ShirtNumber = 37, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 369, Name = "Cristante", TeamId = 34, ShirtNumber = 4, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 370, Name = "Paredes", TeamId = 34, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 371, Name = "Pellegrini", TeamId = 34, ShirtNumber = 7, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 372, Name = "Dybala", TeamId = 34, ShirtNumber = 21, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 373, Name = "Lukaku", TeamId = 34, ShirtNumber = 90, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 374, Name = "El Shaarawy", TeamId = 34, ShirtNumber = 92, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 375, Name = "Szczesny", TeamId = 35, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 376, Name = "Danilo", TeamId = 35, ShirtNumber = 6, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 377, Name = "Bremer", TeamId = 35, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 378, Name = "Gatti", TeamId = 35, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 379, Name = "Cambiaso", TeamId = 35, ShirtNumber = 27, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 380, Name = "Locatelli", TeamId = 35, ShirtNumber = 5, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 381, Name = "Rabiot", TeamId = 35, ShirtNumber = 25, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 382, Name = "McKennie", TeamId = 35, ShirtNumber = 16, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 383, Name = "Chiesa", TeamId = 35, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 384, Name = "Vlahovic", TeamId = 35, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 385, Name = "Yildiz", TeamId = 35, ShirtNumber = 15, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 386, Name = "Skorupski", TeamId = 36, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 387, Name = "Posch", TeamId = 36, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 388, Name = "Beukema", TeamId = 36, ShirtNumber = 31, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 389, Name = "Lucumi", TeamId = 36, ShirtNumber = 26, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 390, Name = "Kristiansen", TeamId = 36, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 391, Name = "Freuler", TeamId = 36, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 392, Name = "Ferguson", TeamId = 36, ShirtNumber = 19, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 393, Name = "Aebischer", TeamId = 36, ShirtNumber = 20, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 394, Name = "Orsolini", TeamId = 36, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 395, Name = "Zirkzee", TeamId = 36, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 396, Name = "Ndoye", TeamId = 36, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 397, Name = "Audero", TeamId = 37, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 398, Name = "Ioannou", TeamId = 37, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 399, Name = "Barba", TeamId = 37, ShirtNumber = 93, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 400, Name = "Goldaniga", TeamId = 37, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 401, Name = "Sala", TeamId = 37, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 402, Name = "Bellemo", TeamId = 37, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 403, Name = "Da Cunha", TeamId = 37, ShirtNumber = 33, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 404, Name = "Baselli", TeamId = 37, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 405, Name = "Cutrone", TeamId = 37, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 406, Name = "Gabrielloni", TeamId = 37, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 407, Name = "Verdi", TeamId = 37, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 408, Name = "Provedel", TeamId = 38, ShirtNumber = 94, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 409, Name = "Lazzari", TeamId = 38, ShirtNumber = 29, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 410, Name = "Romagnoli", TeamId = 38, ShirtNumber = 13, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 411, Name = "Casale", TeamId = 38, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 412, Name = "Marusic", TeamId = 38, ShirtNumber = 77, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 413, Name = "Guendouzi", TeamId = 38, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 414, Name = "Luis Alberto", TeamId = 38, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 415, Name = "Kamada", TeamId = 38, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 416, Name = "Felipe Anderson", TeamId = 38, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 417, Name = "Immobile", TeamId = 38, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 418, Name = "Zaccagni", TeamId = 38, ShirtNumber = 20, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 419, Name = "Consigli", TeamId = 39, ShirtNumber = 47, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 420, Name = "Toljan", TeamId = 39, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 421, Name = "Erlic", TeamId = 39, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 422, Name = "Ruan", TeamId = 39, ShirtNumber = 44, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 423, Name = "Vina", TeamId = 39, ShirtNumber = 17, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 424, Name = "Boloca", TeamId = 39, ShirtNumber = 24, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 425, Name = "Thorstevedt", TeamId = 39, ShirtNumber = 42, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 426, Name = "Bajrami", TeamId = 39, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 427, Name = "Berardi", TeamId = 39, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 428, Name = "Pinamonti", TeamId = 39, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 429, Name = "Lauriente", TeamId = 39, ShirtNumber = 45, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 430, Name = "Silvestri", TeamId = 40, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 431, Name = "Perez", TeamId = 40, ShirtNumber = 13, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 432, Name = "Bijol", TeamId = 40, ShirtNumber = 29, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 433, Name = "Kristensen", TeamId = 40, ShirtNumber = 31, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 434, Name = "Kamara", TeamId = 40, ShirtNumber = 11, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 435, Name = "Walace", TeamId = 40, ShirtNumber = 5, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 436, Name = "Samardzic", TeamId = 40, ShirtNumber = 24, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 437, Name = "Payero", TeamId = 40, ShirtNumber = 32, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 438, Name = "Pereyra", TeamId = 40, ShirtNumber = 37, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 439, Name = "Lucca", TeamId = 40, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 440, Name = "Success", TeamId = 40, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 441, Name = "Donnarumma", TeamId = 41, ShirtNumber = 99, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 442, Name = "Hakimi", TeamId = 41, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 443, Name = "Marquinhos", TeamId = 41, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 444, Name = "Skriniar", TeamId = 41, ShirtNumber = 37, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 445, Name = "Lucas Hernandez", TeamId = 41, ShirtNumber = 21, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 446, Name = "Ugarte", TeamId = 41, ShirtNumber = 4, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 447, Name = "Vitinha", TeamId = 41, ShirtNumber = 17, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 448, Name = "Zaire-Emery", TeamId = 41, ShirtNumber = 33, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 449, Name = "Dembele", TeamId = 41, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 450, Name = "Ramos", TeamId = 41, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 451, Name = "Barcola", TeamId = 41, ShirtNumber = 29, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 452, Name = "Samba", TeamId = 42, ShirtNumber = 30, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 453, Name = "Gradit", TeamId = 42, ShirtNumber = 24, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 454, Name = "Danso", TeamId = 42, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 455, Name = "Medina", TeamId = 42, ShirtNumber = 14, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 456, Name = "Machado", TeamId = 42, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 457, Name = "Abdul Samed", TeamId = 42, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 458, Name = "Thomasson", TeamId = 42, ShirtNumber = 28, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 459, Name = "Fulgini", TeamId = 42, ShirtNumber = 11, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 460, Name = "Sotoca", TeamId = 42, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 461, Name = "Wahi", TeamId = 42, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 462, Name = "Pereira da Costa", TeamId = 42, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 463, Name = "Lopez", TeamId = 43, ShirtNumber = 16, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 464, Name = "Clauss", TeamId = 43, ShirtNumber = 7, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 465, Name = "Mbemba", TeamId = 43, ShirtNumber = 99, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 466, Name = "Gigot", TeamId = 43, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 467, Name = "Lodi", TeamId = 43, ShirtNumber = 12, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 468, Name = "Veretout", TeamId = 43, ShirtNumber = 27, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 469, Name = "Kondogbia", TeamId = 43, ShirtNumber = 19, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 470, Name = "Harit", TeamId = 43, ShirtNumber = 11, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 471, Name = "Aubameyang", TeamId = 43, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 472, Name = "Ismaila Sarr", TeamId = 43, ShirtNumber = 23, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 473, Name = "Vitinha", TeamId = 43, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },

new Player { Id = 474, Name = "Chevalier", TeamId = 44, ShirtNumber = 30, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 475, Name = "Diakite", TeamId = 44, ShirtNumber = 18, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 476, Name = "Yoro", TeamId = 44, ShirtNumber = 15, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 477, Name = "Alexsandro", TeamId = 44, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 478, Name = "Ismaily", TeamId = 44, ShirtNumber = 31, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 479, Name = "Andre", TeamId = 44, ShirtNumber = 21, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 480, Name = "Cabella", TeamId = 44, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 481, Name = "Bentaleb", TeamId = 44, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 482, Name = "David", TeamId = 44, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 483, Name = "Zhegrova", TeamId = 44, ShirtNumber = 23, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 484, Name = "Yazici", TeamId = 44, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 485, Name = "Lopes", TeamId = 45, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 486, Name = "Mata", TeamId = 45, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 487, Name = "Lovren", TeamId = 45, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 488, Name = "OBrien", TeamId = 45, ShirtNumber = 12, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 489, Name = "Tagliafico", TeamId = 45, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 490, Name = "Caqueret", TeamId = 45, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 491, Name = "Tolisso", TeamId = 45, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 492, Name = "Cherki", TeamId = 45, ShirtNumber = 18, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 493, Name = "Lacazette", TeamId = 45, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 494, Name = "Nuamah", TeamId = 45, ShirtNumber = 37, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 495, Name = "Benrahma", TeamId = 45, ShirtNumber = 17, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 496, Name = "Mandanda", TeamId = 46, ShirtNumber = 30, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 497, Name = "Assignon", TeamId = 46, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 498, Name = "Theate", TeamId = 46, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 499, Name = "Omari", TeamId = 46, ShirtNumber = 23, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 500, Name = "Truffert", TeamId = 46, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 501, Name = "Matic", TeamId = 46, ShirtNumber = 21, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 502, Name = "Le Fee", TeamId = 46, ShirtNumber = 28, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 503, Name = "Bourigeaud", TeamId = 46, ShirtNumber = 14, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 504, Name = "Terrier", TeamId = 46, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 505, Name = "Gouiri", TeamId = 46, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 506, Name = "Blas", TeamId = 46, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 507, Name = "Sels", TeamId = 47, ShirtNumber = 1, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 508, Name = "Perrin", TeamId = 47, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 509, Name = "Nyamsi", TeamId = 47, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 510, Name = "Sylla", TeamId = 47, ShirtNumber = 77, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 511, Name = "Delaine", TeamId = 47, ShirtNumber = 29, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 512, Name = "Sissoko", TeamId = 47, ShirtNumber = 27, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 513, Name = "Diarra", TeamId = 47, ShirtNumber = 19, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 514, Name = "Gameiro", TeamId = 47, ShirtNumber = 9, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 515, Name = "Emegha", TeamId = 47, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 516, Name = "Aholou", TeamId = 47, ShirtNumber = 6, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 517, Name = "Bakari", TeamId = 47, ShirtNumber = 14, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 518, Name = "Restes", TeamId = 48, ShirtNumber = 50, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 519, Name = "Desler", TeamId = 48, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 520, Name = "Nicolaisen", TeamId = 48, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 521, Name = "Costa", TeamId = 48, ShirtNumber = 4, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 522, Name = "Suazo", TeamId = 48, ShirtNumber = 17, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 523, Name = "Sierro", TeamId = 48, ShirtNumber = 8, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 524, Name = "Spierings", TeamId = 48, ShirtNumber = 21, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 525, Name = "Casseres", TeamId = 48, ShirtNumber = 23, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 526, Name = "Dallinga", TeamId = 48, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 527, Name = "Magri", TeamId = 48, ShirtNumber = 13, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 528, Name = "Gboho", TeamId = 48, ShirtNumber = 10, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 529, Name = "Kohn", TeamId = 49, ShirtNumber = 16, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 530, Name = "Vanderson", TeamId = 49, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 531, Name = "Maripan", TeamId = 49, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 532, Name = "Kehrer", TeamId = 49, ShirtNumber = 5, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 533, Name = "Henrique", TeamId = 49, ShirtNumber = 12, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 534, Name = "Zakaria", TeamId = 49, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 535, Name = "Fofana", TeamId = 49, ShirtNumber = 19, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 536, Name = "Golovin", TeamId = 49, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 537, Name = "Minamino", TeamId = 49, ShirtNumber = 18, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 538, Name = "Ben Yedder", TeamId = 49, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 539, Name = "Balogun", TeamId = 49, ShirtNumber = 29, Position = PlayerPosition.Forward, IsActive = true },

new Player { Id = 540, Name = "Fofana", TeamId = 50, ShirtNumber = 30, Position = PlayerPosition.Goalkeeper, IsActive = true },

new Player { Id = 541, Name = "Valery", TeamId = 50, ShirtNumber = 2, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 542, Name = "Bamba", TeamId = 50, ShirtNumber = 25, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 543, Name = "Hountondji", TeamId = 50, ShirtNumber = 22, Position = PlayerPosition.Defender, IsActive = true },
new Player { Id = 544, Name = "Doumbia", TeamId = 50, ShirtNumber = 3, Position = PlayerPosition.Defender, IsActive = true },

new Player { Id = 545, Name = "Mendy", TeamId = 50, ShirtNumber = 6, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 546, Name = "Abdelli", TeamId = 50, ShirtNumber = 10, Position = PlayerPosition.Midfielder, IsActive = true },
new Player { Id = 547, Name = "Capelle", TeamId = 50, ShirtNumber = 15, Position = PlayerPosition.Midfielder, IsActive = true },

new Player { Id = 548, Name = "Hunou", TeamId = 50, ShirtNumber = 7, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 549, Name = "Niane", TeamId = 50, ShirtNumber = 9, Position = PlayerPosition.Forward, IsActive = true },
new Player { Id = 550, Name = "Sima", TeamId = 50, ShirtNumber = 11, Position = PlayerPosition.Forward, IsActive = true }
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