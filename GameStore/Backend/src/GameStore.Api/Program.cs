using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
// these lines are all about configuring this build
var app = builder.Build();

List<Game> games =
[
    new Game {
        Id = Guid.NewGuid(),
        Name = "Street Fighter II",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateOnly(1992, 7, 15)
    },
    new Game {
        Id = Guid.NewGuid(),
        Name = "Final Fantasy XIV",
        Genre = "Roleplaying",
        Price = 59.99M,
        ReleaseDate = new DateOnly(2010, 9, 30)
    },
    new Game {
        Id = Guid.NewGuid(),
        Name = "World of Warcraft",
        Genre = "Massive Multiplayer Online",
        Price = 49.99M,
        ReleaseDate = new DateOnly(2004, 11, 23)
    }
];

// the request pipeline

// GET /games (pattern)
app.MapGet("/games", () => games);
// in this case we say any requests that arrive, 
// we will reply with Hello World!
app.Run();
