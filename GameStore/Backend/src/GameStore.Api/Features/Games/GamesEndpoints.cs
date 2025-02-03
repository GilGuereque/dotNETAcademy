using System;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.DeleteGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.UpdateGame;

namespace GameStore.Api.Features.Games;

public static class GamesEndpoints
{
    public static void MapGames(
        this IEndpointRouteBuilder app,
        GameStoreData data
    )
    {
        var group = app.MapGroup("/games");

        group.MapGetGames(data);
        group.MapGetGame(data);
        group.MapCreateGame(data);
        group.MapUpdateGame(data);
        group.MapDeleteGame(data);
    }
}
