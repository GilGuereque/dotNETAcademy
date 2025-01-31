using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
// these lines are all about configuring this build
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

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

// GET /games/id
app.MapGet("/games/{id}", (Guid id) =>
{
    Console.WriteLine($"Searching for game with ID: {id}");

    Game? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName); //Identify endpoint url for GET

// POST /games
app.MapPost("/games", (Game game) =>
{
    if (string.IsNullOrEmpty(game.Name))
    {
        return Results.BadRequest("Name is required in the payload.");
    }

    game.Id = Guid.NewGuid();
    games.Add(game);

    return Results.CreatedAtRoute(
        GetGameEndpointName,
        new { id = game.Id },
        game);
})
.WithParameterValidation();

// PUT /games/id
app.MapPut("/games/{id}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
})
.WithParameterValidation();

// DELETE /games/id
app.MapDelete("/games/{id}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
