
using GameStore.Api.Authorization;
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
        group.MapGet("/", async (IGamesRepository repository) => (await repository.GetAllAsync()).Select(game => game.AsDto()));

        //GET request to get game by id
        //group.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

        //GET request to get game by id (handle both cases valid and invalid id)
        group.MapGet("/{id}", async (int id, IGamesRepository repository) =>
        {
            Game? game = await repository.GetAsync(id);
            /* if (game == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game); */
            //refactoring above code in one line
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();

        }).WithName(GetGameEndpointName)
        .RequireAuthorization(Policies.ReadAccess);


        //POST request to create resource
        group.MapPost("/", async (CreateGameDto gameDto, IGamesRepository repository) =>
        {

            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUrl = gameDto.ImageUrl
            };

            await repository.CreateAsync(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        }).RequireAuthorization(Policies.WriteAccess);

        //PUT request to update record
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGameDto, IGamesRepository repository) =>
        {
            Game? existingGame = await repository.GetAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGameDto.Name;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUrl = updatedGameDto.ImageUrl;

            await repository.UpdateAsync(existingGame);

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess);

        //DELETE request to delete game
        group.MapDelete("/{id}", async (int id, IGamesRepository repository) =>
        {
            Game? game = await repository.GetAsync(id);
            if (game is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess);

        return group;
    }
}
