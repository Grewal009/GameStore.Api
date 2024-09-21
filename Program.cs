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

var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapGamesEndpoints(); //using extension method

app.Run();
