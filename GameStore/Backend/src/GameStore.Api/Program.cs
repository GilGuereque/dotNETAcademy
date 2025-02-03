using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

GameStoreData data = new();

// All games related endpoints
app.MapGames(data);
// Genres related endpoints
app.MapGenres(data);

app.Run();