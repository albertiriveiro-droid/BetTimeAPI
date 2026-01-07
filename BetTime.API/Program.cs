using BetTime.Data;
using BetTime.Data;
using Microsoft.EntityFrameworkCore;

using BetTime.Business;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BetTime.Services;

;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });



builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IMarketService, MarketService>();
builder.Services.AddSingleton<DynamicOddsService>();
builder.Services.AddScoped<IMarketSelectionService, MarketSelectionService>();
builder.Services.AddHostedService<MatchSimulationService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IBetService, BetService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerMatchStatsService, PlayerMatchStatsService>();
builder.Services.AddScoped<IPlayerMarketService, PlayerMarketService>();
builder.Services.AddScoped<IPlayerMarketSelectionService, PlayerMarketSelectionService>();
builder.Services.AddScoped<IMarketGeneratorService,MarketGeneratorService>();
builder.Services.AddScoped<IPlayerMarketGeneratorService, PlayerMarketGeneratorService>();
builder.Services.AddScoped<IUserRepository, UserEFRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerEFRepository>();
builder.Services.AddScoped<IPlayerMatchStatsRepository, PlayerMatchStatsEFRepository>();
builder.Services.AddScoped<IPlayerMarketRepository, PlayerMarketEFRepository>();
builder.Services.AddScoped<IPlayerMarketSelectionRepository, PlayerMarketSelectionEFRepository>();
builder.Services.AddScoped<ISportRepository, SportEFRepository>();
builder.Services.AddScoped<ILeagueRepository, LeagueEFRepository>();
builder.Services.AddScoped<ITeamRepository, TeamEFRepository>();
builder.Services.AddScoped<IMatchRepository, MatchEFRepository>();
builder.Services.AddScoped<IMarketRepository, MarketEFRepository>();
builder.Services.AddScoped<IMarketSelectionRepository, MarketSelectionEFRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionEFRepository>();
builder.Services.AddScoped<IBetRepository, BetEFRepository>();


var connectionString = builder.Configuration.GetConnectionString("ServerDB_azure");

builder.Services.AddDbContext<BetTimeContext>(options =>
    options.UseSqlServer(connectionString));




builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://zealous-coast-017cd6b03.1.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddHttpClient();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BetTime API", Version = "v1" });



    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();


    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BetTimeContext>();
        context.Database.Migrate();
    }


app.UseCors("MyAllowedOrigins");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

