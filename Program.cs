using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//only one instance for the entire lifecycle
//builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepository>();

//using EntityFrameworkGamesRepository instead of InMemoryGamesRepository
builder.Services.AddScoped<IGamesRepository, EntityFrameworkGamesRepository>();

// reading connection string from user-secrets
var connString = builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

app.Services.InitializeDb();

app.MapGamesEndpoints(); //using extension method

app.Run();
