using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.DeleteGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.UpdateGame;
using GameStore.Api.Features.Genres.GetGenres;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

GameStoreData data = new();

// GET /games (retrieve all existing games)
app.MapGetGames(data);
// GET /games/{id} (retrieve one game & its details)
app.MapGetGame(data);
// POST /games (create game)
app.MapCreateGame(data);
// PUT /games/{id} (update game)
app.MapUpdateGame(data);
// DELETE /games/{id} (delete specific game)
app.MapDeleteGame(data);
// GET /genres (retreive all existing genres)
app.MapGetGenres(data);

app.Run();