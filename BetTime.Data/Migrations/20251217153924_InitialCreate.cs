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
                    { 3, "Premier League", 1 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Date", "Note", "PaymentMethod", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 50m, new DateTime(2025, 12, 15, 15, 39, 24, 316, DateTimeKind.Utc).AddTicks(7389), null, "Tarjeta", "DEPOSIT", 1 },
                    { 2, 25m, new DateTime(2025, 12, 16, 15, 39, 24, 316, DateTimeKind.Utc).AddTicks(7394), null, "PayPal", "DEPOSIT", 1 },
                    { 3, 100m, new DateTime(2025, 12, 14, 15, 39, 24, 316, DateTimeKind.Utc).AddTicks(7395), null, "Tarjeta", "DEPOSIT", 2 },
                    { 4, 50m, new DateTime(2025, 12, 16, 15, 39, 24, 316, DateTimeKind.Utc).AddTicks(7397), null, "PayPal", "WITHDRAW", 2 },
                    { 5, 75m, new DateTime(2025, 12, 15, 15, 39, 24, 316, DateTimeKind.Utc).AddTicks(7398), null, "Tarjeta", "DEPOSIT", 3 },
                    { 6, 30m, new DateTime(2025, 12, 16, 15, 39, 24, 316, DateTimeKind.Utc).AddTicks(7399), null, "PayPal", "WITHDRAW", 3 }
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
                    { 6, 2, "Bayern Munich" },
                    { 7, 2, "Borussia Dortmund" },
                    { 8, 2, "RB Leipzig" },
                    { 9, 2, "Bayer Leverkusen" },
                    { 10, 2, "Schalke 04" },
                    { 11, 3, "Manchester United" },
                    { 12, 3, "Liverpool" },
                    { 13, 3, "Chelsea" },
                    { 14, 3, "Arsenal" },
                    { 15, 3, "Manchester City" }
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
                    { 1, true, "Courtois", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 2, true, "Carvajal", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 3, true, "Militao", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 4, true, "Alaba", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 5, true, "Mendy", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 6, true, "Modric", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 7, true, "Casemiro", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 8, true, "Kroos", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 9, true, "Vinicius", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 10, true, "Benzema", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 11, true, "Rodrygo", 0, 0, 1, 0, 0, 0, 0, 0 },
                    { 12, true, "Ter Stegen", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 13, true, "Dest", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 14, true, "Piqué", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 15, true, "Araujo", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 16, true, "Alba", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 17, true, "Busquets", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 18, true, "Pedri", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 19, true, "Gavi", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 20, true, "Lewandowski", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 21, true, "Ferran Torres", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 22, true, "Raphinha", 0, 0, 2, 0, 0, 0, 0, 0 },
                    { 23, true, "Oblak", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 24, true, "Trippier", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 25, true, "Giménez", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 26, true, "Savic", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 27, true, "Reinildo", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 28, true, "Koke", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 29, true, "De Paul", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 30, true, "Saul", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 31, true, "Griezmann", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 32, true, "Correa", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 33, true, "Felix", 0, 0, 3, 0, 0, 0, 0, 0 },
                    { 34, true, "Bono", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 35, true, "Navas", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 36, true, "Koundé", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 37, true, "Diego Carlos", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 38, true, "Acuna", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 39, true, "Fernando", 0, 0, 4, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "IsActive", "Name", "Position", "ShirtNumber", "TeamId", "TempAssists", "TempGoals", "TempMinutesPlayed", "TempRedCards", "TempYellowCards" },
                values: new object[,]
                {
                    { 40, true, "Rakitic", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 41, true, "Joan Jordán", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 42, true, "En-Nesyri", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 43, true, "Ocampos", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 44, true, "Martínez", 0, 0, 4, 0, 0, 0, 0, 0 },
                    { 45, true, "Cillessen", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 46, true, "Gaya", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 47, true, "Alderete", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 48, true, "Diakhaby", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 49, true, "Foulquier", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 50, true, "Soler", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 51, true, "Cáceres", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 52, true, "Guedes", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 53, true, "D. Almeida", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 54, true, "Moreno", 0, 0, 5, 0, 0, 0, 0, 0 },
                    { 55, true, "Cheryshev", 0, 0, 5, 0, 0, 0, 0, 0 }
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
