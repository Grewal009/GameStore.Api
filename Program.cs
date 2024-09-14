using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

//only one instance for the entire lifecycle
builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepository>();

var connString = builder.Configuration.GetConnectionString("GameStoreContext");

var app = builder.Build();

app.MapGamesEndpoints(); //using extension method

app.Run();
