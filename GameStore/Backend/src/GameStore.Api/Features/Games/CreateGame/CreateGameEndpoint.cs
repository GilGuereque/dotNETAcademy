using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Features.Genres.GetGenres;
using GameStore.Api.Models;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder app)
    {
        // POST /games (add a new game)
        app.MapPost("/", async (
            CreateGameDto gameDto,
            GameStoreContext dbContext) =>
        {
            var game = new Game
            {
                Name = gameDto.Name,
                GenreId = gameDto.GenreId,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                Description = gameDto.Description
            };

            dbContext.Games.Add(game);

            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                EndpointNames.GetGame,
                new { id = game.Id },
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate,
                    game.Description
                ));
        })
        .WithParameterValidation();
    }
}
