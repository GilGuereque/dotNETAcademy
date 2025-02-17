using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        // GET /games/id (retrieve a specific game)
        app.MapGet("/{id}", async (
            Guid id,
            GameStoreContext dbContext,
            ILogger<Program> Logger) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate,
                    game.Description
                )
            );
        })
        .WithName(EndpointNames.GetGame); //Identify endpoint url for GET (Assigns name to the endpoint)
    }
}
