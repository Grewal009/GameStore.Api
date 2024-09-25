using GameStore.Api.Authorization;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

//only one instance for the entire lifecycle
//builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepository>();

//using EntityFrameworkGamesRepository instead of InMemoryGamesRepository
//builder.Services.AddScoped<IGamesRepository, EntityFrameworkGamesRepository>();

// reading connection string from user-secrets
//var connString = builder.Configuration.GetConnectionString("GameStoreContext");
//builder.Services.AddSqlServer<GameStoreContext>(connString);

builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.ReadAccess, builder => builder.RequireClaim("scope", "games:read"));

    options.AddPolicy(Policies.WriteAccess, builder => builder.RequireClaim("scope", "games:write").RequireRole("Admin"));

});

var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapGamesEndpoints(); //using extension method

app.Run();
