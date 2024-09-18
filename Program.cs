using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//only one instance for the entire lifecycle
builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepository>();

// reading connection string from user-secrets
var connString = builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    dbContext.Database.Migrate();
}

app.MapGamesEndpoints(); //using extension method

app.Run();
