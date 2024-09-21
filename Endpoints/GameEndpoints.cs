
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{


    const string GetGameEndpointName = "GetGame";
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        //InMemoryGamesRepository repository = new();

        var group = routes.MapGroup("/games")
            .WithParameterValidation(); //inforce validations to all endpoints using nuget package (MinimalApis.Extensions)

        //GET request to get all games
        group.MapGet("/", (IGamesRepository repository) => repository.GetAllAsync().Select(game => game.AsDto()));

        //GET request to get game by id
        //group.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

        //GET request to get game by id (handle both cases valid and invalid id)
        group.MapGet("/{id}", (int id, IGamesRepository repository) =>
        {
            Game? game = repository.GetAsync(id);
            /* if (game == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game); */
            //refactoring above code in one line
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();

        }).WithName(GetGameEndpointName);


        //POST request to create resource
        group.MapPost("/", (CreateGameDto gameDto, IGamesRepository repository) =>
        {

            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUrl = gameDto.ImageUrl
            };

            repository.CreateAsync(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        //PUT request to update record
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGameDto, IGamesRepository repository) =>
        {
            Game? existingGame = repository.GetAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGameDto.Name;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUrl = updatedGameDto.ImageUrl;

            repository.UpdateAsync(existingGame);

            return Results.NoContent();
        });

        //DELETE request to delete game
        group.MapDelete("/{id}", (int id, IGamesRepository repository) =>
        {
            Game? game = repository.GetAsync(id);
            if (game is not null)
            {
                repository.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}
