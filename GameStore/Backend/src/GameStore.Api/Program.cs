using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

// REGISTER SERVICES HERE
// You must register the services before you build constructing the app
builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddSingleton<GameStoreData>();

var app = builder.Build();

// All games related endpoints
app.MapGames();
// Genres related endpoints
app.MapGenres();

app.Run();