using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;


public class InMemoryGamesRepository : IGamesRepository
{

    private readonly List<Game> games = new(){
    new Game { Id = 1, Name = "Streetfighter", Genre= "Fighting", Price = 19.99M, ReleaseDate = new DateTime(2021,2,5),ImageUrl= "https://placeholder.co/100"},
    new Game { Id = 2, Name = "Final Fantasy", Genre= "Roleplaying", Price = 29.99M, ReleaseDate = new DateTime(2022,10,15),ImageUrl= "https://placeholder.co/100"},
    new Game { Id = 3, Name = "FIFA 2023", Genre= "Sports", Price = 49.99M, ReleaseDate = new DateTime(2023,12,25),ImageUrl= "https://placeholder.co/100"},
    };

    //get all games 
    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    //get one game by id
    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(game => game.Id == id));
    }

    //create a game
    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
        await Task.CompletedTask;
    }

    //update game based on id
    public async Task UpdateAsync(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;
        await Task.CompletedTask;
    }

    //delete game
    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
        await Task.CompletedTask;
    }

}
