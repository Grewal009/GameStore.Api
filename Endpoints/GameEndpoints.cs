
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{


    const string GetGameEndpointName = "GetGame";
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemoryGamesRepository repository = new();
        var group = routes.MapGroup("/games")
            .WithParameterValidation(); //inforce validations to all endpoints using nuget package (MinimalApis.Extensions)

        //GET request to get all games
        group.MapGet("/", () => repository.GetAll());

        //GET request to get game by id
        //group.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

        //GET request to get game by id (handle both cases valid and invalid id)
        group.MapGet("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);
            /* if (game == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game); */
            //refactoring above code in one line
            return game is not null ? Results.Ok(game) : Results.NotFound();

        }).WithName(GetGameEndpointName);


        //POST request to create resource
        group.MapPost("/", (Game game) =>
        {
            repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        //PUT request to update record
        group.MapPut("/{id}", (int id, Game updatedGame) =>
        {
            Game? existingGame = repository.Get(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGame.Name;
            existingGame.Genre = updatedGame.Genre;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.ImageUrl = updatedGame.ImageUrl;

            repository.Update(existingGame);

            return Results.NoContent();
        });

        //DELETE request to delete game
        group.MapDelete("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);
            if (game is not null)
            {
                repository.Delete(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}
