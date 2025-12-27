using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetTime.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leagues_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeScore = table.Column<int>(type: "int", nullable: false),
                    AwayScore = table.Column<int>(type: "int", nullable: false),
                    HomeCorners = table.Column<int>(type: "int", nullable: false),
                    AwayCorners = table.Column<int>(type: "int", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    PlayerMatchStatsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    ShirtNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TempGoals = table.Column<int>(type: "int", nullable: false),
                    TempAssists = table.Column<int>(type: "int", nullable: false),
                    TempMinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    TempYellowCards = table.Column<int>(type: "int", nullable: false),
                    TempRedCards = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    MarketType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMarkets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    PlayerMarketType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMarkets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMarkets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerMarkets_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMatchStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    MatchId1 = table.Column<int>(type: "int", nullable: true),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    YellowCard = table.Column<int>(type: "int", nullable: false),
                    RedCard = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMatchStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMatchStats_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerMatchStats_Matches_MatchId1",
                        column: x => x.MatchId1,
                        principalTable: "Matches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerMatchStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketSelections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Threshold = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketSelections_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMarketSelections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerMarketId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Odd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Threshold = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMarketSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMarketSelections_PlayerMarkets_PlayerMarketId",
                        column: x => x.PlayerMarketId,
                        principalTable: "PlayerMarkets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    MarketSelectionId = table.Column<int>(type: "int", nullable: true),
                    PlayerMarketSelectionId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Won = table.Column<bool>(type: "bit", nullable: true),
                    PlacedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_MarketSelections_MarketSelectionId",
                        column: x => x.MarketSelectionId,
                        principalTable: "MarketSelections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bets_PlayerMarketSelections_PlayerMarketSelectionId",
                        column: x => x.PlayerMarketSelectionId,
                        principalTable: "PlayerMarketSelections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Sports",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Fútbol" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CreatedAt", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, 100m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "juan@example.com", "pass123", "user", "juan123" },
                    { 2, 150m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "albertoriveiro@hotmail.es", "pass456", "admin", "albertorbd" },
                    { 3, 120m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura@example.com", "pass789", "user", "laura234" }
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name", "SportId" },
                values: new object[,]
                {
                    { 1, "LaLiga", 1 },
                    { 2, "Bundesliga", 1 },
                    { 3, "Premier League", 1 },
                    { 4, "Serie A", 1 },
                    { 5, "Ligue 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Date", "Note", "PaymentMethod", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 50m, new DateTime(2025, 12, 23, 18, 22, 53, 395, DateTimeKind.Utc).AddTicks(9996), null, "Tarjeta", "DEPOSIT", 1 },
                    { 2, 25m, new DateTime(2025, 12, 24, 18, 22, 53, 396, DateTimeKind.Utc).AddTicks(2), null, "PayPal", "DEPOSIT", 1 },
                    { 3, 100m, new DateTime(2025, 12, 22, 18, 22, 53, 396, DateTimeKind.Utc).AddTicks(3), null, "Tarjeta", "DEPOSIT", 2 },
                    { 4, 50m, new DateTime(2025, 12, 24, 18, 22, 53, 396, DateTimeKind.Utc).AddTicks(4), null, "PayPal", "WITHDRAW", 2 },
                    { 5, 75m, new DateTime(2025, 12, 23, 18, 22, 53, 396, DateTimeKind.Utc).AddTicks(6), null, "Tarjeta", "DEPOSIT", 3 },
                    { 6, 30m, new DateTime(2025, 12, 24, 18, 22, 53, 396, DateTimeKind.Utc).AddTicks(7), null, "PayPal", "WITHDRAW", 3 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "LeagueId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Real Madrid" },
                    { 2, 1, "Barcelona" },
                    { 3, 1, "Atlético Madrid" },
                    { 4, 1, "Sevilla" },
                    { 5, 1, "Valencia" },
                    { 6, 1, "Villarreal" },
                    { 7, 1, "RCD Espanyol" },
                    { 8, 1, "Real Betis" },
                    { 9, 1, "Athletic" },
                    { 10, 1, "Celta de Vigo" },
                    { 11, 2, "Bayern Munich" },
                    { 12, 2, "Borussia Dortmund" },
                    { 13, 2, "RB Leipzig" },
                    { 14, 2, "Bayer Leverkusen" },
                    { 15, 2, "Mainz 05" },
                    { 16, 2, "Hoffenheim" },
                    { 17, 2, "Stuttgart" },
                    { 18, 2, "Frankfurt" },
                    { 19, 2, "FC Union Berlin" },
                    { 20, 2, "Friburgo" },
                    { 21, 3, "Manchester United" },
                    { 22, 3, "Liverpool" },
                    { 23, 3, "Chelsea" },
                    { 24, 3, "Arsenal" },
                    { 25, 3, "Manchester City" },
                    { 26, 3, "Crystal Palace" },
                    { 27, 3, "Sunderland" },
                    { 28, 3, "Everton" },
                    { 29, 3, "Brighton" },
                    { 30, 3, "Tottenham" },
                    { 31, 4, "Milan" },
                    { 32, 4, "Inter" },
                    { 33, 4, "Napoli" },
                    { 34, 4, "Roma" },
                    { 35, 4, "Juventus" },
                    { 36, 4, "Bolonia" },
                    { 37, 4, "Como" },
                    { 38, 4, "Lazio" },
                    { 39, 4, "Sassuolo" },
                    { 40, 4, "Udinese" },
                    { 41, 5, "PSG" },
                    { 42, 5, "Lens" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "LeagueId", "Name" },
                values: new object[,]
                {
                    { 43, 5, "Marsella" },
                    { 44, 5, "LOSC" },
                    { 45, 5, "Lyon" },
                    { 46, 5, "Rennes" },
                    { 47, 5, "Racing de Estrasburgo" },
                    { 48, 5, "Toulouse" },
                    { 49, 5, "Mónaco" },
                    { 50, 5, "Angers" }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "AwayCorners", "AwayScore", "AwayTeamId", "DurationMinutes", "Finished", "HomeCorners", "HomeScore", "HomeTeamId", "LeagueId", "PlayerMatchStatsJson", "StartTime" },
                values: new object[,]
                {
                    { 1, 0, 0, 2, 90, false, 0, 0, 1, 1, null, new DateTime(2025, 12, 11, 21, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 0, 0, 7, 90, false, 0, 0, 6, 2, null, new DateTime(2025, 12, 11, 21, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 0, 0, 12, 90, false, 0, 0, 11, 3, null, new DateTime(2025, 12, 11, 21, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 1, true, "Courtois", 0, 1, 1, 0, 0, 0, 0, 0 },
                    { 2, true, "Carvajal", 1, 2, 1, 0, 0, 0, 0, 0 },
                    { 3, true, "Militao", 1, 3, 1, 0, 0, 0, 0, 0 },
                    { 4, true, "Alaba", 1, 4, 1, 0, 0, 0, 0, 0 },
                    { 5, true, "Mendy", 1, 23, 1, 0, 0, 0, 0, 0 },
                    { 6, true, "Tchouameni", 2, 18, 1, 0, 0, 0, 0, 0 },
                    { 7, true, "Valverde", 2, 15, 1, 0, 0, 0, 0, 0 },
                    { 8, true, "Bellingham", 2, 5, 1, 0, 0, 0, 0, 0 },
                    { 9, true, "Rodrygo", 3, 11, 1, 0, 0, 0, 0, 0 },
                    { 10, true, "Mbappe", 3, 9, 1, 0, 0, 0, 0, 0 },
                    { 11, true, "Vinicius", 3, 7, 1, 0, 0, 0, 0, 0 },
                    { 12, true, "Ter Stegen", 0, 1, 2, 0, 0, 0, 0, 0 },
                    { 13, true, "Kounde", 1, 23, 2, 0, 0, 0, 0, 0 },
                    { 14, true, "Araujo", 1, 4, 2, 0, 0, 0, 0, 0 },
                    { 15, true, "Christensen", 1, 15, 2, 0, 0, 0, 0, 0 },
                    { 16, true, "Balde", 1, 3, 2, 0, 0, 0, 0, 0 },
                    { 17, true, "De Jong", 2, 21, 2, 0, 0, 0, 0, 0 },
                    { 18, true, "Pedri", 2, 8, 2, 0, 0, 0, 0, 0 },
                    { 19, true, "Gavi", 2, 6, 2, 0, 0, 0, 0, 0 },
                    { 20, true, "Yamal", 3, 27, 2, 0, 0, 0, 0, 0 },
                    { 21, true, "Lewandowski", 3, 9, 2, 0, 0, 0, 0, 0 },
                    { 22, true, "Raphinha", 3, 11, 2, 0, 0, 0, 0, 0 },
                    { 23, true, "Oblak", 0, 13, 3, 0, 0, 0, 0, 0 },
                    { 24, true, "Molina", 1, 16, 3, 0, 0, 0, 0, 0 },
                    { 25, true, "Gimenez", 1, 2, 3, 0, 0, 0, 0, 0 },
                    { 26, true, "Savic", 1, 15, 3, 0, 0, 0, 0, 0 },
                    { 27, true, "Reinildo", 1, 23, 3, 0, 0, 0, 0, 0 },
                    { 28, true, "Koke", 2, 6, 3, 0, 0, 0, 0, 0 },
                    { 29, true, "De Paul", 2, 5, 3, 0, 0, 0, 0, 0 },
                    { 30, true, "Saul", 2, 8, 3, 0, 0, 0, 0, 0 },
                    { 31, true, "Griezmann", 3, 7, 3, 0, 0, 0, 0, 0 },
                    { 32, true, "Morata", 3, 19, 3, 0, 0, 0, 0, 0 },
                    { 33, true, "Correa", 3, 10, 3, 0, 0, 0, 0, 0 },
                    { 34, true, "Nyland", 0, 13, 4, 0, 0, 0, 0, 0 },
                    { 35, true, "Navas", 1, 16, 4, 0, 0, 0, 0, 0 },
                    { 36, true, "Ramos", 1, 4, 4, 0, 0, 0, 0, 0 },
                    { 37, true, "Bade", 1, 22, 4, 0, 0, 0, 0, 0 },
                    { 38, true, "Acuna", 1, 19, 4, 0, 0, 0, 0, 0 },
                    { 39, true, "Soumare", 2, 6, 4, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 40, true, "Rakitic", 2, 10, 4, 0, 0, 0, 0, 0 },
                    { 41, true, "Ocampos", 2, 5, 4, 0, 0, 0, 0, 0 },
                    { 42, true, "Suso", 3, 7, 4, 0, 0, 0, 0, 0 },
                    { 43, true, "En-Nesyri", 3, 15, 4, 0, 0, 0, 0, 0 },
                    { 44, true, "Lamela", 3, 17, 4, 0, 0, 0, 0, 0 },
                    { 45, true, "Mamardashvili", 0, 25, 5, 0, 0, 0, 0, 0 },
                    { 46, true, "Correia", 1, 2, 5, 0, 0, 0, 0, 0 },
                    { 47, true, "Mosquera", 1, 3, 5, 0, 0, 0, 0, 0 },
                    { 48, true, "Diakhaby", 1, 4, 5, 0, 0, 0, 0, 0 },
                    { 49, true, "Gaya", 1, 14, 5, 0, 0, 0, 0, 0 },
                    { 50, true, "Pepelu", 2, 18, 5, 0, 0, 0, 0, 0 },
                    { 51, true, "Javi Guerra", 2, 8, 5, 0, 0, 0, 0, 0 },
                    { 52, true, "Almeida", 2, 10, 5, 0, 0, 0, 0, 0 },
                    { 53, true, "Diego Lopez", 3, 16, 5, 0, 0, 0, 0, 0 },
                    { 54, true, "Hugo Duro", 3, 9, 5, 0, 0, 0, 0, 0 },
                    { 55, true, "Canos", 3, 11, 5, 0, 0, 0, 0, 0 },
                    { 56, true, "Jorgensen", 0, 13, 6, 0, 0, 0, 0, 0 },
                    { 57, true, "Foyth", 1, 8, 6, 0, 0, 0, 0, 0 },
                    { 58, true, "Albiol", 1, 3, 6, 0, 0, 0, 0, 0 },
                    { 59, true, "Cuenca", 1, 5, 6, 0, 0, 0, 0, 0 },
                    { 60, true, "Pedraza", 1, 24, 6, 0, 0, 0, 0, 0 },
                    { 61, true, "Parejo", 2, 10, 6, 0, 0, 0, 0, 0 },
                    { 62, true, "Capoue", 2, 6, 6, 0, 0, 0, 0, 0 },
                    { 63, true, "Baena", 2, 16, 6, 0, 0, 0, 0, 0 },
                    { 64, true, "Guedes", 3, 17, 6, 0, 0, 0, 0, 0 },
                    { 65, true, "Gerard Moreno", 3, 7, 6, 0, 0, 0, 0, 0 },
                    { 66, true, "Sorloth", 3, 9, 6, 0, 0, 0, 0, 0 },
                    { 67, true, "Pacheco", 0, 13, 7, 0, 0, 0, 0, 0 },
                    { 68, true, "Oscar Gil", 1, 20, 7, 0, 0, 0, 0, 0 },
                    { 69, true, "Cabrera", 1, 6, 7, 0, 0, 0, 0, 0 },
                    { 70, true, "Calero", 1, 5, 7, 0, 0, 0, 0, 0 },
                    { 71, true, "Brian Olivan", 1, 14, 7, 0, 0, 0, 0, 0 },
                    { 72, true, "Expósito", 2, 8, 7, 0, 0, 0, 0, 0 },
                    { 73, true, "Gragera", 2, 15, 7, 0, 0, 0, 0, 0 },
                    { 74, true, "Melamed", 2, 21, 7, 0, 0, 0, 0, 0 },
                    { 75, true, "Puado", 3, 7, 7, 0, 0, 0, 0, 0 },
                    { 76, true, "Braithwaite", 3, 22, 7, 0, 0, 0, 0, 0 },
                    { 77, true, "Jofre", 3, 11, 7, 0, 0, 0, 0, 0 },
                    { 78, true, "Rui Silva", 0, 13, 8, 0, 0, 0, 0, 0 },
                    { 79, true, "Bellerin", 1, 2, 8, 0, 0, 0, 0, 0 },
                    { 80, true, "Pezzella", 1, 6, 8, 0, 0, 0, 0, 0 },
                    { 81, true, "Bartra", 1, 5, 8, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 82, true, "Miranda", 1, 33, 8, 0, 0, 0, 0, 0 },
                    { 83, true, "Guido Rodriguez", 2, 4, 8, 0, 0, 0, 0, 0 },
                    { 84, true, "Isco", 2, 22, 8, 0, 0, 0, 0, 0 },
                    { 85, true, "Fekir", 2, 8, 8, 0, 0, 0, 0, 0 },
                    { 86, true, "Ayoze", 3, 10, 8, 0, 0, 0, 0, 0 },
                    { 87, true, "Borja Iglesias", 3, 9, 8, 0, 0, 0, 0, 0 },
                    { 88, true, "Luiz Henrique", 3, 11, 8, 0, 0, 0, 0, 0 },
                    { 89, true, "Unai Simon", 0, 1, 9, 0, 0, 0, 0, 0 },
                    { 90, true, "De Marcos", 1, 18, 9, 0, 0, 0, 0, 0 },
                    { 91, true, "Vivian", 1, 3, 9, 0, 0, 0, 0, 0 },
                    { 92, true, "Yeray", 1, 5, 9, 0, 0, 0, 0, 0 },
                    { 93, true, "Yuri", 1, 17, 9, 0, 0, 0, 0, 0 },
                    { 94, true, "Vesga", 2, 6, 9, 0, 0, 0, 0, 0 },
                    { 95, true, "Sancet", 2, 8, 9, 0, 0, 0, 0, 0 },
                    { 96, true, "Muniain", 2, 10, 9, 0, 0, 0, 0, 0 },
                    { 97, true, "Nico Williams", 3, 11, 9, 0, 0, 0, 0, 0 },
                    { 98, true, "Guruzeta", 3, 12, 9, 0, 0, 0, 0, 0 },
                    { 99, true, "Inaki Williams", 3, 9, 9, 0, 0, 0, 0, 0 },
                    { 100, true, "Guaita", 0, 13, 10, 0, 0, 0, 0, 0 },
                    { 101, true, "Mallo", 1, 2, 10, 0, 0, 0, 0, 0 },
                    { 102, true, "Starfelt", 1, 24, 10, 0, 0, 0, 0, 0 },
                    { 103, true, "Aidoo", 1, 15, 10, 0, 0, 0, 0, 0 },
                    { 104, true, "Mingueza", 1, 3, 10, 0, 0, 0, 0, 0 },
                    { 105, true, "Beltran", 2, 8, 10, 0, 0, 0, 0, 0 },
                    { 106, true, "De la Torre", 2, 14, 10, 0, 0, 0, 0, 0 },
                    { 107, true, "Bamba", 2, 17, 10, 0, 0, 0, 0, 0 },
                    { 108, true, "Aspas", 3, 10, 10, 0, 0, 0, 0, 0 },
                    { 109, true, "Strand Larsen", 3, 18, 10, 0, 0, 0, 0, 0 },
                    { 110, true, "Douvikas", 3, 9, 10, 0, 0, 0, 0, 0 },
                    { 111, true, "Neuer", 0, 1, 11, 0, 0, 0, 0, 0 },
                    { 112, true, "Kimmich", 1, 6, 11, 0, 0, 0, 0, 0 },
                    { 113, true, "De Ligt", 1, 4, 11, 0, 0, 0, 0, 0 },
                    { 114, true, "Upamecano", 1, 2, 11, 0, 0, 0, 0, 0 },
                    { 115, true, "Davies", 1, 19, 11, 0, 0, 0, 0, 0 },
                    { 116, true, "Goretzka", 2, 8, 11, 0, 0, 0, 0, 0 },
                    { 117, true, "Laimer", 2, 27, 11, 0, 0, 0, 0, 0 },
                    { 118, true, "Musiala", 2, 42, 11, 0, 0, 0, 0, 0 },
                    { 119, true, "Sane", 3, 10, 11, 0, 0, 0, 0, 0 },
                    { 120, true, "Kane", 3, 9, 11, 0, 0, 0, 0, 0 },
                    { 121, true, "Coman", 3, 11, 11, 0, 0, 0, 0, 0 },
                    { 122, true, "Kobel", 0, 1, 12, 0, 0, 0, 0, 0 },
                    { 123, true, "Ryerson", 1, 26, 12, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 124, true, "Hummels", 1, 15, 12, 0, 0, 0, 0, 0 },
                    { 125, true, "Schlotterbeck", 1, 4, 12, 0, 0, 0, 0, 0 },
                    { 126, true, "Bensebaini", 1, 5, 12, 0, 0, 0, 0, 0 },
                    { 127, true, "Can", 2, 23, 12, 0, 0, 0, 0, 0 },
                    { 128, true, "Brandt", 2, 19, 12, 0, 0, 0, 0, 0 },
                    { 129, true, "Sabitzer", 2, 20, 12, 0, 0, 0, 0, 0 },
                    { 130, true, "Adeyemi", 3, 27, 12, 0, 0, 0, 0, 0 },
                    { 131, true, "Füllkrug", 3, 14, 12, 0, 0, 0, 0, 0 },
                    { 132, true, "Malen", 3, 21, 12, 0, 0, 0, 0, 0 },
                    { 133, true, "Gulacsi", 0, 1, 13, 0, 0, 0, 0, 0 },
                    { 134, true, "Henrichs", 1, 39, 13, 0, 0, 0, 0, 0 },
                    { 135, true, "Orban", 1, 4, 13, 0, 0, 0, 0, 0 },
                    { 136, true, "Simakan", 1, 2, 13, 0, 0, 0, 0, 0 },
                    { 137, true, "Raum", 1, 22, 13, 0, 0, 0, 0, 0 },
                    { 138, true, "Schlager", 2, 24, 13, 0, 0, 0, 0, 0 },
                    { 139, true, "Haidara", 2, 8, 13, 0, 0, 0, 0, 0 },
                    { 140, true, "Olmo", 2, 7, 13, 0, 0, 0, 0, 0 },
                    { 141, true, "Openda", 3, 17, 13, 0, 0, 0, 0, 0 },
                    { 142, true, "Sesko", 3, 30, 13, 0, 0, 0, 0, 0 },
                    { 143, true, "Simons", 3, 20, 13, 0, 0, 0, 0, 0 },
                    { 144, true, "Hradecky", 0, 1, 14, 0, 0, 0, 0, 0 },
                    { 145, true, "Frimpong", 1, 30, 14, 0, 0, 0, 0, 0 },
                    { 146, true, "Tah", 1, 4, 14, 0, 0, 0, 0, 0 },
                    { 147, true, "Tapsoba", 1, 12, 14, 0, 0, 0, 0, 0 },
                    { 148, true, "Grimaldo", 1, 20, 14, 0, 0, 0, 0, 0 },
                    { 149, true, "Xhaka", 2, 34, 14, 0, 0, 0, 0, 0 },
                    { 150, true, "Palacios", 2, 25, 14, 0, 0, 0, 0, 0 },
                    { 151, true, "Wirtz", 2, 10, 14, 0, 0, 0, 0, 0 },
                    { 152, true, "Adli", 3, 21, 14, 0, 0, 0, 0, 0 },
                    { 153, true, "Schick", 3, 14, 14, 0, 0, 0, 0, 0 },
                    { 154, true, "Boniface", 3, 22, 14, 0, 0, 0, 0, 0 },
                    { 155, true, "Zentner", 0, 27, 15, 0, 0, 0, 0, 0 },
                    { 156, true, "Widmer", 1, 30, 15, 0, 0, 0, 0, 0 },
                    { 157, true, "Bell", 1, 16, 15, 0, 0, 0, 0, 0 },
                    { 158, true, "Hanche-Olsen", 1, 25, 15, 0, 0, 0, 0, 0 },
                    { 159, true, "Caci", 1, 19, 15, 0, 0, 0, 0, 0 },
                    { 160, true, "Kohr", 2, 31, 15, 0, 0, 0, 0, 0 },
                    { 161, true, "Barreiro", 2, 8, 15, 0, 0, 0, 0, 0 },
                    { 162, true, "Lee Jae-Sung", 2, 7, 15, 0, 0, 0, 0, 0 },
                    { 163, true, "Burkardt", 3, 29, 15, 0, 0, 0, 0, 0 },
                    { 164, true, "Onisiwo", 3, 9, 15, 0, 0, 0, 0, 0 },
                    { 165, true, "Ajorque", 3, 17, 15, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 166, true, "Baumann", 0, 1, 16, 0, 0, 0, 0, 0 },
                    { 167, true, "Kaderabek", 1, 3, 16, 0, 0, 0, 0, 0 },
                    { 168, true, "Akpoguma", 1, 25, 16, 0, 0, 0, 0, 0 },
                    { 169, true, "Vogt", 1, 22, 16, 0, 0, 0, 0, 0 },
                    { 170, true, "Skov", 1, 29, 16, 0, 0, 0, 0, 0 },
                    { 171, true, "Grillitsch", 2, 11, 16, 0, 0, 0, 0, 0 },
                    { 172, true, "Stach", 2, 16, 16, 0, 0, 0, 0, 0 },
                    { 173, true, "Kramaric", 2, 27, 16, 0, 0, 0, 0, 0 },
                    { 174, true, "Bebou", 3, 9, 16, 0, 0, 0, 0, 0 },
                    { 175, true, "Beier", 3, 14, 16, 0, 0, 0, 0, 0 },
                    { 176, true, "Weghorst", 3, 10, 16, 0, 0, 0, 0, 0 },
                    { 177, true, "Nubel", 0, 33, 17, 0, 0, 0, 0, 0 },
                    { 178, true, "Vagnoman", 1, 4, 17, 0, 0, 0, 0, 0 },
                    { 179, true, "Anton", 1, 2, 17, 0, 0, 0, 0, 0 },
                    { 180, true, "Ito", 1, 21, 17, 0, 0, 0, 0, 0 },
                    { 181, true, "Mittelstadt", 1, 7, 17, 0, 0, 0, 0, 0 },
                    { 182, true, "Karazor", 2, 16, 17, 0, 0, 0, 0, 0 },
                    { 183, true, "Stiller", 2, 6, 17, 0, 0, 0, 0, 0 },
                    { 184, true, "Millot", 2, 8, 17, 0, 0, 0, 0, 0 },
                    { 185, true, "Führich", 3, 27, 17, 0, 0, 0, 0, 0 },
                    { 186, true, "Guirassy", 3, 9, 17, 0, 0, 0, 0, 0 },
                    { 187, true, "Undav", 3, 26, 17, 0, 0, 0, 0, 0 },
                    { 188, true, "Trapp", 0, 1, 18, 0, 0, 0, 0, 0 },
                    { 189, true, "Tuta", 1, 35, 18, 0, 0, 0, 0, 0 },
                    { 190, true, "Koch", 1, 4, 18, 0, 0, 0, 0, 0 },
                    { 191, true, "Pacho", 1, 3, 18, 0, 0, 0, 0, 0 },
                    { 192, true, "Nkounkou", 1, 29, 18, 0, 0, 0, 0, 0 },
                    { 193, true, "Skhiri", 2, 15, 18, 0, 0, 0, 0, 0 },
                    { 194, true, "Larsson", 2, 16, 18, 0, 0, 0, 0, 0 },
                    { 195, true, "Gotze", 2, 27, 18, 0, 0, 0, 0, 0 },
                    { 196, true, "Knauff", 3, 36, 18, 0, 0, 0, 0, 0 },
                    { 197, true, "Ekitike", 3, 11, 18, 0, 0, 0, 0, 0 },
                    { 198, true, "Marmoush", 3, 7, 18, 0, 0, 0, 0, 0 },
                    { 199, true, "Ronnnow", 0, 1, 19, 0, 0, 0, 0, 0 },
                    { 200, true, "Juranovic", 1, 18, 19, 0, 0, 0, 0, 0 },
                    { 201, true, "Knoche", 1, 31, 19, 0, 0, 0, 0, 0 },
                    { 202, true, "Doekhi", 1, 5, 19, 0, 0, 0, 0, 0 },
                    { 203, true, "Leite", 1, 4, 19, 0, 0, 0, 0, 0 },
                    { 204, true, "Khedira", 2, 8, 19, 0, 0, 0, 0, 0 },
                    { 205, true, "Laidouni", 2, 20, 19, 0, 0, 0, 0, 0 },
                    { 206, true, "Haberer", 2, 19, 19, 0, 0, 0, 0, 0 },
                    { 207, true, "Behrens", 3, 17, 19, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 208, true, "Volland", 3, 10, 19, 0, 0, 0, 0, 0 },
                    { 209, true, "Jordan", 3, 11, 19, 0, 0, 0, 0, 0 },
                    { 210, true, "Atubolu", 0, 1, 20, 0, 0, 0, 0, 0 },
                    { 211, true, "Kübler", 1, 17, 20, 0, 0, 0, 0, 0 },
                    { 212, true, "Ginter", 1, 28, 20, 0, 0, 0, 0, 0 },
                    { 213, true, "Lienhart", 1, 3, 20, 0, 0, 0, 0, 0 },
                    { 214, true, "Günther", 1, 30, 20, 0, 0, 0, 0, 0 },
                    { 215, true, "Höfler", 2, 27, 20, 0, 0, 0, 0, 0 },
                    { 216, true, "Eggestein", 2, 8, 20, 0, 0, 0, 0, 0 },
                    { 217, true, "Doan", 2, 42, 20, 0, 0, 0, 0, 0 },
                    { 218, true, "Grifo", 3, 32, 20, 0, 0, 0, 0, 0 },
                    { 219, true, "Gregoritsch", 3, 9, 20, 0, 0, 0, 0, 0 },
                    { 220, true, "Sallai", 3, 22, 20, 0, 0, 0, 0, 0 },
                    { 221, true, "Onana", 0, 24, 21, 0, 0, 0, 0, 0 },
                    { 222, true, "Dalot", 1, 20, 21, 0, 0, 0, 0, 0 },
                    { 223, true, "Varane", 1, 19, 21, 0, 0, 0, 0, 0 },
                    { 224, true, "Martinez", 1, 6, 21, 0, 0, 0, 0, 0 },
                    { 225, true, "Shaw", 1, 23, 21, 0, 0, 0, 0, 0 },
                    { 226, true, "Casemiro", 2, 18, 21, 0, 0, 0, 0, 0 },
                    { 227, true, "Bruno Fernandes", 2, 8, 21, 0, 0, 0, 0, 0 },
                    { 228, true, "Mount", 2, 7, 21, 0, 0, 0, 0, 0 },
                    { 229, true, "Rashford", 3, 10, 21, 0, 0, 0, 0, 0 },
                    { 230, true, "Hojlund", 3, 11, 21, 0, 0, 0, 0, 0 },
                    { 231, true, "Garnacho", 3, 17, 21, 0, 0, 0, 0, 0 },
                    { 232, true, "Alisson", 0, 1, 22, 0, 0, 0, 0, 0 },
                    { 233, true, "Alexander-Arnold", 1, 66, 22, 0, 0, 0, 0, 0 },
                    { 234, true, "Van Dijk", 1, 4, 22, 0, 0, 0, 0, 0 },
                    { 235, true, "Konate", 1, 5, 22, 0, 0, 0, 0, 0 },
                    { 236, true, "Robertson", 1, 26, 22, 0, 0, 0, 0, 0 },
                    { 237, true, "Mac Allister", 2, 10, 22, 0, 0, 0, 0, 0 },
                    { 238, true, "Szoboszlai", 2, 8, 22, 0, 0, 0, 0, 0 },
                    { 239, true, "Gravenberch", 2, 38, 22, 0, 0, 0, 0, 0 },
                    { 240, true, "Salah", 3, 11, 22, 0, 0, 0, 0, 0 },
                    { 241, true, "Nunez", 3, 9, 22, 0, 0, 0, 0, 0 },
                    { 242, true, "Diaz", 3, 7, 22, 0, 0, 0, 0, 0 },
                    { 243, true, "Petrovic", 0, 28, 23, 0, 0, 0, 0, 0 },
                    { 244, true, "James", 1, 24, 23, 0, 0, 0, 0, 0 },
                    { 245, true, "Disasi", 1, 2, 23, 0, 0, 0, 0, 0 },
                    { 246, true, "Colwill", 1, 26, 23, 0, 0, 0, 0, 0 },
                    { 247, true, "Chilwell", 1, 21, 23, 0, 0, 0, 0, 0 },
                    { 248, true, "Caicedo", 2, 25, 23, 0, 0, 0, 0, 0 },
                    { 249, true, "Enzo Fernandez", 2, 8, 23, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 250, true, "Palmer", 2, 20, 23, 0, 0, 0, 0, 0 },
                    { 251, true, "Sterling", 3, 7, 23, 0, 0, 0, 0, 0 },
                    { 252, true, "Jackson", 3, 15, 23, 0, 0, 0, 0, 0 },
                    { 253, true, "Mudryk", 3, 10, 23, 0, 0, 0, 0, 0 },
                    { 254, true, "Raya", 0, 22, 24, 0, 0, 0, 0, 0 },
                    { 255, true, "White", 1, 4, 24, 0, 0, 0, 0, 0 },
                    { 256, true, "Saliba", 1, 2, 24, 0, 0, 0, 0, 0 },
                    { 257, true, "Gabriel", 1, 6, 24, 0, 0, 0, 0, 0 },
                    { 258, true, "Zinchenko", 1, 35, 24, 0, 0, 0, 0, 0 },
                    { 259, true, "Rice", 2, 41, 24, 0, 0, 0, 0, 0 },
                    { 260, true, "Odegaard", 2, 8, 24, 0, 0, 0, 0, 0 },
                    { 261, true, "Havertz", 2, 29, 24, 0, 0, 0, 0, 0 },
                    { 262, true, "Saka", 3, 7, 24, 0, 0, 0, 0, 0 },
                    { 263, true, "Jesus", 3, 9, 24, 0, 0, 0, 0, 0 },
                    { 264, true, "Martinelli", 3, 11, 24, 0, 0, 0, 0, 0 },
                    { 265, true, "Ederson", 0, 31, 25, 0, 0, 0, 0, 0 },
                    { 266, true, "Walker", 1, 2, 25, 0, 0, 0, 0, 0 },
                    { 267, true, "Dias", 1, 3, 25, 0, 0, 0, 0, 0 },
                    { 268, true, "Stones", 1, 5, 25, 0, 0, 0, 0, 0 },
                    { 269, true, "Gvardiol", 1, 24, 25, 0, 0, 0, 0, 0 },
                    { 270, true, "Rodri", 2, 16, 25, 0, 0, 0, 0, 0 },
                    { 271, true, "De Bruyne", 2, 17, 25, 0, 0, 0, 0, 0 },
                    { 272, true, "Foden", 2, 47, 25, 0, 0, 0, 0, 0 },
                    { 273, true, "Silva", 3, 20, 25, 0, 0, 0, 0, 0 },
                    { 274, true, "Haaland", 3, 9, 25, 0, 0, 0, 0, 0 },
                    { 275, true, "Doku", 3, 11, 25, 0, 0, 0, 0, 0 },
                    { 276, true, "Johnstone", 0, 1, 26, 0, 0, 0, 0, 0 },
                    { 277, true, "Clyne", 1, 17, 26, 0, 0, 0, 0, 0 },
                    { 278, true, "Guehi", 1, 6, 26, 0, 0, 0, 0, 0 },
                    { 279, true, "Andersen", 1, 16, 26, 0, 0, 0, 0, 0 },
                    { 280, true, "Mitchell", 1, 3, 26, 0, 0, 0, 0, 0 },
                    { 281, true, "Doucoure", 2, 28, 26, 0, 0, 0, 0, 0 },
                    { 282, true, "Lerma", 2, 8, 26, 0, 0, 0, 0, 0 },
                    { 283, true, "Eze", 2, 10, 26, 0, 0, 0, 0, 0 },
                    { 284, true, "Olise", 3, 7, 26, 0, 0, 0, 0, 0 },
                    { 285, true, "Mateta", 3, 14, 26, 0, 0, 0, 0, 0 },
                    { 286, true, "Ayew", 3, 9, 26, 0, 0, 0, 0, 0 },
                    { 287, true, "Patterson", 0, 1, 27, 0, 0, 0, 0, 0 },
                    { 288, true, "Hume", 1, 32, 27, 0, 0, 0, 0, 0 },
                    { 289, true, "Ballard", 1, 5, 27, 0, 0, 0, 0, 0 },
                    { 290, true, "O'Nien", 1, 13, 27, 0, 0, 0, 0, 0 },
                    { 291, true, "Cirkin", 1, 3, 27, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 292, true, "Neil", 2, 24, 27, 0, 0, 0, 0, 0 },
                    { 293, true, "Ekwah", 2, 14, 27, 0, 0, 0, 0, 0 },
                    { 294, true, "Bellingham", 2, 7, 27, 0, 0, 0, 0, 0 },
                    { 295, true, "Roberts", 3, 10, 27, 0, 0, 0, 0, 0 },
                    { 296, true, "Clarke", 3, 20, 27, 0, 0, 0, 0, 0 },
                    { 297, true, "Burstow", 3, 19, 27, 0, 0, 0, 0, 0 },
                    { 298, true, "Pickford", 0, 1, 28, 0, 0, 0, 0, 0 },
                    { 299, true, "Coleman", 1, 23, 28, 0, 0, 0, 0, 0 },
                    { 300, true, "Tarkowski", 1, 2, 28, 0, 0, 0, 0, 0 },
                    { 301, true, "Branthwaite", 1, 32, 28, 0, 0, 0, 0, 0 },
                    { 302, true, "Mykolenko", 1, 19, 28, 0, 0, 0, 0, 0 },
                    { 303, true, "Gueye", 2, 27, 28, 0, 0, 0, 0, 0 },
                    { 304, true, "Onana", 2, 8, 28, 0, 0, 0, 0, 0 },
                    { 305, true, "Doucoure", 2, 16, 28, 0, 0, 0, 0, 0 },
                    { 306, true, "McNeil", 3, 7, 28, 0, 0, 0, 0, 0 },
                    { 307, true, "Calvert-Lewin", 3, 9, 28, 0, 0, 0, 0, 0 },
                    { 308, true, "Harrison", 3, 11, 28, 0, 0, 0, 0, 0 },
                    { 309, true, "Verbruggen", 0, 1, 29, 0, 0, 0, 0, 0 },
                    { 310, true, "Lamptey", 1, 2, 29, 0, 0, 0, 0, 0 },
                    { 311, true, "Dunk", 1, 5, 29, 0, 0, 0, 0, 0 },
                    { 312, true, "Van Hecke", 1, 29, 29, 0, 0, 0, 0, 0 },
                    { 313, true, "Estupiñan", 1, 30, 29, 0, 0, 0, 0, 0 },
                    { 314, true, "Gross", 2, 13, 29, 0, 0, 0, 0, 0 },
                    { 315, true, "Gilmour", 2, 11, 29, 0, 0, 0, 0, 0 },
                    { 316, true, "Enciso", 2, 10, 29, 0, 0, 0, 0, 0 },
                    { 317, true, "Mitoma", 3, 22, 29, 0, 0, 0, 0, 0 },
                    { 318, true, "Pedro", 3, 9, 29, 0, 0, 0, 0, 0 },
                    { 319, true, "Adingra", 3, 24, 29, 0, 0, 0, 0, 0 },
                    { 320, true, "Vicario", 0, 13, 30, 0, 0, 0, 0, 0 },
                    { 321, true, "Porro", 1, 23, 30, 0, 0, 0, 0, 0 },
                    { 322, true, "Romero", 1, 17, 30, 0, 0, 0, 0, 0 },
                    { 323, true, "Van de Ven", 1, 37, 30, 0, 0, 0, 0, 0 },
                    { 324, true, "Udogie", 1, 38, 30, 0, 0, 0, 0, 0 },
                    { 325, true, "Bissouma", 2, 8, 30, 0, 0, 0, 0, 0 },
                    { 326, true, "Bentancur", 2, 30, 30, 0, 0, 0, 0, 0 },
                    { 327, true, "Maddison", 2, 10, 30, 0, 0, 0, 0, 0 },
                    { 328, true, "Kulusevski", 3, 21, 30, 0, 0, 0, 0, 0 },
                    { 329, true, "Son", 3, 7, 30, 0, 0, 0, 0, 0 },
                    { 330, true, "Richarlison", 3, 9, 30, 0, 0, 0, 0, 0 },
                    { 331, true, "Maignan", 0, 16, 31, 0, 0, 0, 0, 0 },
                    { 332, true, "Calabria", 1, 2, 31, 0, 0, 0, 0, 0 },
                    { 333, true, "Tomori", 1, 23, 31, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 334, true, "Thiaw", 1, 28, 31, 0, 0, 0, 0, 0 },
                    { 335, true, "Theo Hernandez", 1, 19, 31, 0, 0, 0, 0, 0 },
                    { 336, true, "Bennacer", 2, 4, 31, 0, 0, 0, 0, 0 },
                    { 337, true, "Reijnders", 2, 14, 31, 0, 0, 0, 0, 0 },
                    { 338, true, "Pulisic", 2, 11, 31, 0, 0, 0, 0, 0 },
                    { 339, true, "Leao", 3, 10, 31, 0, 0, 0, 0, 0 },
                    { 340, true, "Giroud", 3, 9, 31, 0, 0, 0, 0, 0 },
                    { 341, true, "Chukwueze", 3, 21, 31, 0, 0, 0, 0, 0 },
                    { 342, true, "Sommer", 0, 1, 32, 0, 0, 0, 0, 0 },
                    { 343, true, "Pavard", 1, 28, 32, 0, 0, 0, 0, 0 },
                    { 344, true, "Acerbi", 1, 15, 32, 0, 0, 0, 0, 0 },
                    { 345, true, "Bastoni", 1, 95, 32, 0, 0, 0, 0, 0 },
                    { 346, true, "Darmian", 1, 36, 32, 0, 0, 0, 0, 0 },
                    { 347, true, "Calhanoglu", 2, 20, 32, 0, 0, 0, 0, 0 },
                    { 348, true, "Barella", 2, 23, 32, 0, 0, 0, 0, 0 },
                    { 349, true, "Mkhitaryan", 2, 22, 32, 0, 0, 0, 0, 0 },
                    { 350, true, "Thuram", 3, 9, 32, 0, 0, 0, 0, 0 },
                    { 351, true, "Lautaro Martinez", 3, 10, 32, 0, 0, 0, 0, 0 },
                    { 352, true, "Dimarco", 3, 32, 32, 0, 0, 0, 0, 0 },
                    { 353, true, "Meret", 0, 1, 33, 0, 0, 0, 0, 0 },
                    { 354, true, "Di Lorenzo", 1, 22, 33, 0, 0, 0, 0, 0 },
                    { 355, true, "Rrahmani", 1, 13, 33, 0, 0, 0, 0, 0 },
                    { 356, true, "Juan Jesus", 1, 5, 33, 0, 0, 0, 0, 0 },
                    { 357, true, "Olivera", 1, 17, 33, 0, 0, 0, 0, 0 },
                    { 358, true, "Lobotka", 2, 68, 33, 0, 0, 0, 0, 0 },
                    { 359, true, "Anguissa", 2, 99, 33, 0, 0, 0, 0, 0 },
                    { 360, true, "Zielinski", 2, 20, 33, 0, 0, 0, 0, 0 },
                    { 361, true, "Politano", 3, 21, 33, 0, 0, 0, 0, 0 },
                    { 362, true, "Osimhen", 3, 9, 33, 0, 0, 0, 0, 0 },
                    { 363, true, "Kvaratskhelia", 3, 77, 33, 0, 0, 0, 0, 0 },
                    { 364, true, "Svilar", 0, 99, 34, 0, 0, 0, 0, 0 },
                    { 365, true, "Mancini", 1, 23, 34, 0, 0, 0, 0, 0 },
                    { 366, true, "Smalling", 1, 6, 34, 0, 0, 0, 0, 0 },
                    { 367, true, "Ndicka", 1, 5, 34, 0, 0, 0, 0, 0 },
                    { 368, true, "Spinazzola", 1, 37, 34, 0, 0, 0, 0, 0 },
                    { 369, true, "Cristante", 2, 4, 34, 0, 0, 0, 0, 0 },
                    { 370, true, "Paredes", 2, 16, 34, 0, 0, 0, 0, 0 },
                    { 371, true, "Pellegrini", 2, 7, 34, 0, 0, 0, 0, 0 },
                    { 372, true, "Dybala", 3, 21, 34, 0, 0, 0, 0, 0 },
                    { 373, true, "Lukaku", 3, 90, 34, 0, 0, 0, 0, 0 },
                    { 374, true, "El Shaarawy", 3, 92, 34, 0, 0, 0, 0, 0 },
                    { 375, true, "Szczesny", 0, 1, 35, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 376, true, "Danilo", 1, 6, 35, 0, 0, 0, 0, 0 },
                    { 377, true, "Bremer", 1, 3, 35, 0, 0, 0, 0, 0 },
                    { 378, true, "Gatti", 1, 4, 35, 0, 0, 0, 0, 0 },
                    { 379, true, "Cambiaso", 1, 27, 35, 0, 0, 0, 0, 0 },
                    { 380, true, "Locatelli", 2, 5, 35, 0, 0, 0, 0, 0 },
                    { 381, true, "Rabiot", 2, 25, 35, 0, 0, 0, 0, 0 },
                    { 382, true, "McKennie", 2, 16, 35, 0, 0, 0, 0, 0 },
                    { 383, true, "Chiesa", 3, 7, 35, 0, 0, 0, 0, 0 },
                    { 384, true, "Vlahovic", 3, 9, 35, 0, 0, 0, 0, 0 },
                    { 385, true, "Yildiz", 3, 15, 35, 0, 0, 0, 0, 0 },
                    { 386, true, "Skorupski", 0, 1, 36, 0, 0, 0, 0, 0 },
                    { 387, true, "Posch", 1, 3, 36, 0, 0, 0, 0, 0 },
                    { 388, true, "Beukema", 1, 31, 36, 0, 0, 0, 0, 0 },
                    { 389, true, "Lucumi", 1, 26, 36, 0, 0, 0, 0, 0 },
                    { 390, true, "Kristiansen", 1, 15, 36, 0, 0, 0, 0, 0 },
                    { 391, true, "Freuler", 2, 8, 36, 0, 0, 0, 0, 0 },
                    { 392, true, "Ferguson", 2, 19, 36, 0, 0, 0, 0, 0 },
                    { 393, true, "Aebischer", 2, 20, 36, 0, 0, 0, 0, 0 },
                    { 394, true, "Orsolini", 3, 7, 36, 0, 0, 0, 0, 0 },
                    { 395, true, "Zirkzee", 3, 9, 36, 0, 0, 0, 0, 0 },
                    { 396, true, "Ndoye", 3, 11, 36, 0, 0, 0, 0, 0 },
                    { 397, true, "Audero", 0, 1, 37, 0, 0, 0, 0, 0 },
                    { 398, true, "Ioannou", 1, 2, 37, 0, 0, 0, 0, 0 },
                    { 399, true, "Barba", 1, 93, 37, 0, 0, 0, 0, 0 },
                    { 400, true, "Goldaniga", 1, 5, 37, 0, 0, 0, 0, 0 },
                    { 401, true, "Sala", 1, 3, 37, 0, 0, 0, 0, 0 },
                    { 402, true, "Bellemo", 2, 10, 37, 0, 0, 0, 0, 0 },
                    { 403, true, "Da Cunha", 2, 33, 37, 0, 0, 0, 0, 0 },
                    { 404, true, "Baselli", 2, 8, 37, 0, 0, 0, 0, 0 },
                    { 405, true, "Cutrone", 3, 9, 37, 0, 0, 0, 0, 0 },
                    { 406, true, "Gabrielloni", 3, 11, 37, 0, 0, 0, 0, 0 },
                    { 407, true, "Verdi", 3, 7, 37, 0, 0, 0, 0, 0 },
                    { 408, true, "Provedel", 0, 94, 38, 0, 0, 0, 0, 0 },
                    { 409, true, "Lazzari", 1, 29, 38, 0, 0, 0, 0, 0 },
                    { 410, true, "Romagnoli", 1, 13, 38, 0, 0, 0, 0, 0 },
                    { 411, true, "Casale", 1, 15, 38, 0, 0, 0, 0, 0 },
                    { 412, true, "Marusic", 1, 77, 38, 0, 0, 0, 0, 0 },
                    { 413, true, "Guendouzi", 2, 8, 38, 0, 0, 0, 0, 0 },
                    { 414, true, "Luis Alberto", 2, 10, 38, 0, 0, 0, 0, 0 },
                    { 415, true, "Kamada", 2, 6, 38, 0, 0, 0, 0, 0 },
                    { 416, true, "Felipe Anderson", 3, 7, 38, 0, 0, 0, 0, 0 },
                    { 417, true, "Immobile", 3, 17, 38, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 418, true, "Zaccagni", 3, 20, 38, 0, 0, 0, 0, 0 },
                    { 419, true, "Consigli", 0, 47, 39, 0, 0, 0, 0, 0 },
                    { 420, true, "Toljan", 1, 2, 39, 0, 0, 0, 0, 0 },
                    { 421, true, "Erlic", 1, 5, 39, 0, 0, 0, 0, 0 },
                    { 422, true, "Ruan", 1, 44, 39, 0, 0, 0, 0, 0 },
                    { 423, true, "Vina", 1, 17, 39, 0, 0, 0, 0, 0 },
                    { 424, true, "Boloca", 2, 24, 39, 0, 0, 0, 0, 0 },
                    { 425, true, "Thorstevedt", 2, 42, 39, 0, 0, 0, 0, 0 },
                    { 426, true, "Bajrami", 2, 10, 39, 0, 0, 0, 0, 0 },
                    { 427, true, "Berardi", 3, 10, 39, 0, 0, 0, 0, 0 },
                    { 428, true, "Pinamonti", 3, 9, 39, 0, 0, 0, 0, 0 },
                    { 429, true, "Lauriente", 3, 45, 39, 0, 0, 0, 0, 0 },
                    { 430, true, "Silvestri", 0, 1, 40, 0, 0, 0, 0, 0 },
                    { 431, true, "Perez", 1, 13, 40, 0, 0, 0, 0, 0 },
                    { 432, true, "Bijol", 1, 29, 40, 0, 0, 0, 0, 0 },
                    { 433, true, "Kristensen", 1, 31, 40, 0, 0, 0, 0, 0 },
                    { 434, true, "Kamara", 1, 11, 40, 0, 0, 0, 0, 0 },
                    { 435, true, "Walace", 2, 5, 40, 0, 0, 0, 0, 0 },
                    { 436, true, "Samardzic", 2, 24, 40, 0, 0, 0, 0, 0 },
                    { 437, true, "Payero", 2, 32, 40, 0, 0, 0, 0, 0 },
                    { 438, true, "Pereyra", 3, 37, 40, 0, 0, 0, 0, 0 },
                    { 439, true, "Lucca", 3, 17, 40, 0, 0, 0, 0, 0 },
                    { 440, true, "Success", 3, 7, 40, 0, 0, 0, 0, 0 },
                    { 441, true, "Donnarumma", 0, 99, 41, 0, 0, 0, 0, 0 },
                    { 442, true, "Hakimi", 1, 2, 41, 0, 0, 0, 0, 0 },
                    { 443, true, "Marquinhos", 1, 5, 41, 0, 0, 0, 0, 0 },
                    { 444, true, "Skriniar", 1, 37, 41, 0, 0, 0, 0, 0 },
                    { 445, true, "Lucas Hernandez", 1, 21, 41, 0, 0, 0, 0, 0 },
                    { 446, true, "Ugarte", 2, 4, 41, 0, 0, 0, 0, 0 },
                    { 447, true, "Vitinha", 2, 17, 41, 0, 0, 0, 0, 0 },
                    { 448, true, "Zaire-Emery", 2, 33, 41, 0, 0, 0, 0, 0 },
                    { 449, true, "Dembele", 3, 10, 41, 0, 0, 0, 0, 0 },
                    { 450, true, "Ramos", 3, 9, 41, 0, 0, 0, 0, 0 },
                    { 451, true, "Barcola", 3, 29, 41, 0, 0, 0, 0, 0 },
                    { 452, true, "Samba", 0, 30, 42, 0, 0, 0, 0, 0 },
                    { 453, true, "Gradit", 1, 24, 42, 0, 0, 0, 0, 0 },
                    { 454, true, "Danso", 1, 4, 42, 0, 0, 0, 0, 0 },
                    { 455, true, "Medina", 1, 14, 42, 0, 0, 0, 0, 0 },
                    { 456, true, "Machado", 1, 3, 42, 0, 0, 0, 0, 0 },
                    { 457, true, "Abdul Samed", 2, 6, 42, 0, 0, 0, 0, 0 },
                    { 458, true, "Thomasson", 2, 28, 42, 0, 0, 0, 0, 0 },
                    { 459, true, "Fulgini", 2, 11, 42, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 460, true, "Sotoca", 3, 7, 42, 0, 0, 0, 0, 0 },
                    { 461, true, "Wahi", 3, 9, 42, 0, 0, 0, 0, 0 },
                    { 462, true, "Pereira da Costa", 3, 10, 42, 0, 0, 0, 0, 0 },
                    { 463, true, "Lopez", 0, 16, 43, 0, 0, 0, 0, 0 },
                    { 464, true, "Clauss", 1, 7, 43, 0, 0, 0, 0, 0 },
                    { 465, true, "Mbemba", 1, 99, 43, 0, 0, 0, 0, 0 },
                    { 466, true, "Gigot", 1, 4, 43, 0, 0, 0, 0, 0 },
                    { 467, true, "Lodi", 1, 12, 43, 0, 0, 0, 0, 0 },
                    { 468, true, "Veretout", 2, 27, 43, 0, 0, 0, 0, 0 },
                    { 469, true, "Kondogbia", 2, 19, 43, 0, 0, 0, 0, 0 },
                    { 470, true, "Harit", 2, 11, 43, 0, 0, 0, 0, 0 },
                    { 471, true, "Aubameyang", 3, 10, 43, 0, 0, 0, 0, 0 },
                    { 472, true, "Ismaila Sarr", 3, 23, 43, 0, 0, 0, 0, 0 },
                    { 473, true, "Vitinha", 3, 9, 43, 0, 0, 0, 0, 0 },
                    { 474, true, "Chevalier", 0, 30, 44, 0, 0, 0, 0, 0 },
                    { 475, true, "Diakite", 1, 18, 44, 0, 0, 0, 0, 0 },
                    { 476, true, "Yoro", 1, 15, 44, 0, 0, 0, 0, 0 },
                    { 477, true, "Alexsandro", 1, 4, 44, 0, 0, 0, 0, 0 },
                    { 478, true, "Ismaily", 1, 31, 44, 0, 0, 0, 0, 0 },
                    { 479, true, "Andre", 2, 21, 44, 0, 0, 0, 0, 0 },
                    { 480, true, "Cabella", 2, 10, 44, 0, 0, 0, 0, 0 },
                    { 481, true, "Bentaleb", 2, 6, 44, 0, 0, 0, 0, 0 },
                    { 482, true, "David", 3, 9, 44, 0, 0, 0, 0, 0 },
                    { 483, true, "Zhegrova", 3, 23, 44, 0, 0, 0, 0, 0 },
                    { 484, true, "Yazici", 3, 11, 44, 0, 0, 0, 0, 0 },
                    { 485, true, "Lopes", 0, 1, 45, 0, 0, 0, 0, 0 },
                    { 486, true, "Mata", 1, 22, 45, 0, 0, 0, 0, 0 },
                    { 487, true, "Lovren", 1, 5, 45, 0, 0, 0, 0, 0 },
                    { 488, true, "OBrien", 1, 12, 45, 0, 0, 0, 0, 0 },
                    { 489, true, "Tagliafico", 1, 3, 45, 0, 0, 0, 0, 0 },
                    { 490, true, "Caqueret", 2, 6, 45, 0, 0, 0, 0, 0 },
                    { 491, true, "Tolisso", 2, 8, 45, 0, 0, 0, 0, 0 },
                    { 492, true, "Cherki", 2, 18, 45, 0, 0, 0, 0, 0 },
                    { 493, true, "Lacazette", 3, 10, 45, 0, 0, 0, 0, 0 },
                    { 494, true, "Nuamah", 3, 37, 45, 0, 0, 0, 0, 0 },
                    { 495, true, "Benrahma", 3, 17, 45, 0, 0, 0, 0, 0 },
                    { 496, true, "Mandanda", 0, 30, 46, 0, 0, 0, 0, 0 },
                    { 497, true, "Assignon", 1, 22, 46, 0, 0, 0, 0, 0 },
                    { 498, true, "Theate", 1, 5, 46, 0, 0, 0, 0, 0 },
                    { 499, true, "Omari", 1, 23, 46, 0, 0, 0, 0, 0 },
                    { 500, true, "Truffert", 1, 3, 46, 0, 0, 0, 0, 0 },
                    { 501, true, "Matic", 2, 21, 46, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 502, true, "Le Fee", 2, 28, 46, 0, 0, 0, 0, 0 },
                    { 503, true, "Bourigeaud", 2, 14, 46, 0, 0, 0, 0, 0 },
                    { 504, true, "Terrier", 3, 7, 46, 0, 0, 0, 0, 0 },
                    { 505, true, "Gouiri", 3, 9, 46, 0, 0, 0, 0, 0 },
                    { 506, true, "Blas", 3, 11, 46, 0, 0, 0, 0, 0 },
                    { 507, true, "Sels", 0, 1, 47, 0, 0, 0, 0, 0 },
                    { 508, true, "Perrin", 1, 5, 47, 0, 0, 0, 0, 0 },
                    { 509, true, "Nyamsi", 1, 22, 47, 0, 0, 0, 0, 0 },
                    { 510, true, "Sylla", 1, 77, 47, 0, 0, 0, 0, 0 },
                    { 511, true, "Delaine", 1, 29, 47, 0, 0, 0, 0, 0 },
                    { 512, true, "Sissoko", 2, 27, 47, 0, 0, 0, 0, 0 },
                    { 513, true, "Diarra", 2, 19, 47, 0, 0, 0, 0, 0 },
                    { 514, true, "Gameiro", 2, 9, 47, 0, 0, 0, 0, 0 },
                    { 515, true, "Emegha", 3, 10, 47, 0, 0, 0, 0, 0 },
                    { 516, true, "Aholou", 3, 6, 47, 0, 0, 0, 0, 0 },
                    { 517, true, "Bakari", 3, 14, 47, 0, 0, 0, 0, 0 },
                    { 518, true, "Restes", 0, 50, 48, 0, 0, 0, 0, 0 },
                    { 519, true, "Desler", 1, 3, 48, 0, 0, 0, 0, 0 },
                    { 520, true, "Nicolaisen", 1, 2, 48, 0, 0, 0, 0, 0 },
                    { 521, true, "Costa", 1, 4, 48, 0, 0, 0, 0, 0 },
                    { 522, true, "Suazo", 1, 17, 48, 0, 0, 0, 0, 0 },
                    { 523, true, "Sierro", 2, 8, 48, 0, 0, 0, 0, 0 },
                    { 524, true, "Spierings", 2, 21, 48, 0, 0, 0, 0, 0 },
                    { 525, true, "Casseres", 2, 23, 48, 0, 0, 0, 0, 0 },
                    { 526, true, "Dallinga", 3, 9, 48, 0, 0, 0, 0, 0 },
                    { 527, true, "Magri", 3, 13, 48, 0, 0, 0, 0, 0 },
                    { 528, true, "Gboho", 3, 10, 48, 0, 0, 0, 0, 0 },
                    { 529, true, "Kohn", 0, 16, 49, 0, 0, 0, 0, 0 },
                    { 530, true, "Vanderson", 1, 2, 49, 0, 0, 0, 0, 0 },
                    { 531, true, "Maripan", 1, 3, 49, 0, 0, 0, 0, 0 },
                    { 532, true, "Kehrer", 1, 5, 49, 0, 0, 0, 0, 0 },
                    { 533, true, "Henrique", 1, 12, 49, 0, 0, 0, 0, 0 },
                    { 534, true, "Zakaria", 2, 6, 49, 0, 0, 0, 0, 0 },
                    { 535, true, "Fofana", 2, 19, 49, 0, 0, 0, 0, 0 },
                    { 536, true, "Golovin", 2, 10, 49, 0, 0, 0, 0, 0 },
                    { 537, true, "Minamino", 3, 18, 49, 0, 0, 0, 0, 0 },
                    { 538, true, "Ben Yedder", 3, 9, 49, 0, 0, 0, 0, 0 },
                    { 539, true, "Balogun", 3, 29, 49, 0, 0, 0, 0, 0 },
                    { 540, true, "Fofana", 0, 30, 50, 0, 0, 0, 0, 0 },
                    { 541, true, "Valery", 1, 2, 50, 0, 0, 0, 0, 0 },
                    { 542, true, "Bamba", 1, 25, 50, 0, 0, 0, 0, 0 },
                    { 543, true, "Hountondji", 1, 22, 50, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 544, true, "Doumbia", 1, 3, 50, 0, 0, 0, 0, 0 },
                    { 545, true, "Mendy", 2, 6, 50, 0, 0, 0, 0, 0 },
                    { 546, true, "Abdelli", 2, 10, 50, 0, 0, 0, 0, 0 },
                    { 547, true, "Capelle", 2, 15, 50, 0, 0, 0, 0, 0 },
                    { 548, true, "Hunou", 3, 7, 50, 0, 0, 0, 0, 0 },
                    { 549, true, "Niane", 3, 9, 50, 0, 0, 0, 0, 0 },
                    { 550, true, "Sima", 3, 11, 50, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Markets",
                columns: new[] { "Id", "Description", "MarketType", "MatchId" },
                values: new object[,]
                {
                    { 1, "Resultado final", 0, 1 },
                    { 2, "Más/Menos de 2.5 goles", 1, 1 },
                    { 3, "Resultado final", 0, 2 },
                    { 4, "Más/Menos de 2.5 goles", 1, 2 },
                    { 5, "Resultado final", 0, 3 },
                    { 6, "Más/Menos de 2.5 goles", 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "MarketSelections",
                columns: new[] { "Id", "MarketId", "Name", "Odd", "Threshold" },
                values: new object[,]
                {
                    { 1, 1, "Real Madrid gana", 1.8m, null },
                    { 2, 1, "Empate", 3.2m, null },
                    { 3, 1, "Barcelona gana", 4.0m, null },
                    { 4, 2, "Más de 2.5 goles", 2.1m, null },
                    { 5, 2, "Menos de 2.5 goles", 1.7m, null },
                    { 6, 3, "Bayern Munich gana", 1.5m, null },
                    { 7, 3, "Empate", 3.5m, null },
                    { 8, 3, "Borussia Dortmund gana", 5.0m, null },
                    { 9, 4, "Más de 2.5 goles", 1.9m, null },
                    { 10, 4, "Menos de 2.5 goles", 2.0m, null },
                    { 11, 5, "Manchester United gana", 2.0m, null },
                    { 12, 5, "Empate", 3.0m, null },
                    { 13, 5, "Liverpool gana", 3.5m, null },
                    { 14, 6, "Más de 2.5 goles", 2.2m, null },
                    { 15, 6, "Menos de 2.5 goles", 1.65m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_MarketSelectionId",
                table: "Bets",
                column: "MarketSelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_MatchId",
                table: "Bets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_PlayerMarketSelectionId",
                table: "Bets",
                column: "PlayerMarketSelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_MatchId",
                table: "Markets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketSelections_MarketId",
                table: "MarketSelections",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMarkets_MatchId",
                table: "PlayerMarkets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMarkets_PlayerId",
                table: "PlayerMarkets",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMarketSelections_PlayerMarketId",
                table: "PlayerMarketSelections",
                column: "PlayerMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStats_MatchId",
                table: "PlayerMatchStats",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStats_MatchId1",
                table: "PlayerMatchStats",
                column: "MatchId1");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStats_PlayerId",
                table: "PlayerMatchStats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "PlayerMatchStats");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "MarketSelections");

            migrationBuilder.DropTable(
                name: "PlayerMarketSelections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropTable(
                name: "PlayerMarkets");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
