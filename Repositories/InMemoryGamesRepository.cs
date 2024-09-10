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
    public IEnumerable<Game> GetAll()
    {
        return games;
    }

    //get one game by id
    public Game? Get(int id)
    {
        return games.Find(game => game.Id == id);
    }

    //create a game
    public void Create(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
    }

    //update game based on id
    public void Update(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;
    }

    //delete game
    public void Delete(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
    }

}
