
using GameStore.Api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

List<Game> games = new(){
    new Game { Id = 1, Name = "Streetfighter", Genre= "Fighting", Price = 19.99M, ReleaseDate = new DateTime(2021,2,5),ImageUrl= "https://placeholder.co/100"},
    new Game { Id = 2, Name = "Final Fantasy", Genre= "Roleplaying", Price = 29.99M, ReleaseDate = new DateTime(2022,10,15),ImageUrl= "https://placeholder.co/100"},
    new Game { Id = 3, Name = "FIFA 2023", Genre= "Sports", Price = 49.99M, ReleaseDate = new DateTime(2023,12,25),ImageUrl= "https://placeholder.co/100"},
};

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var group = app.MapGroup("/games")
            .WithParameterValidation(); //inforce validations to all endpoints using nuget package (MinimalApis.Extensions)

const string GetGameEndpointName = "GetGame";

//GET request to get all games
group.MapGet("/", () => games);

//GET request to get game by id
//group.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

//GET request to get game by id (handle both cases valid and invalid id)
group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);
    if (game == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
}).WithName(GetGameEndpointName);

//POST request to create resource
group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

//PUT request to update record
group.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;
    existingGame.ImageUrl = updatedGame.ImageUrl;

    return Results.NoContent();
});

//DELETE request to delete game
group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);
    if (game is not null)
    {
        games.Remove(game);
    }

    return Results.NoContent();
});

app.Run();
