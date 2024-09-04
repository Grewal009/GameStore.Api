
using GameStore.Api.Entities;

List<Game> games = new(){
    new Game { Id = 1, Name = "Streetfighter", Genre= "Fighting", Price = 19.99M, ReleaseDate = new DateTime(2021,2,5),ImageUrl= "https://placeholder.co/100"},
    new Game { Id = 2, Name = "Final Fantasy", Genre= "Roleplaying", Price = 29.99M, ReleaseDate = new DateTime(2022,10,15),ImageUrl= "https://placeholder.co/100"},
    new Game { Id = 3, Name = "FIFA 2023", Genre= "Sports", Price = 49.99M, ReleaseDate = new DateTime(2023,12,25),ImageUrl= "https://placeholder.co/100"},
};

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/games", () => games);

app.Run();
