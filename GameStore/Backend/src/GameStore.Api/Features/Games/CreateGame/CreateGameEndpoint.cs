using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Features.Genres.GetGenres;
using GameStore.Api.Models;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    private const string DefaultImageUri = "https://placehold.co/125";
    public static void MapCreateGame(this IEndpointRouteBuilder app)
    {
        // POST /games (add a new game)
        app.MapPost("/", async (
            [FromForm] CreateGameDto gameDto,
            GameStoreContext dbContext,
            ILogger<Program> logger,
            FileUploader fileUploader) =>
        {
            var imageUri = DefaultImageUri;

            if (gameDto.ImageFile is not null)
            {
                var fileUploadResult = await fileUploader.UploadFileAsync(
                    gameDto.ImageFile,
                    StorageNames.GameImagesFolder
                );

                if (!fileUploadResult.IsSuccess)
                {
                    return Results.BadRequest(new { message = fileUploadResult.ErrorMessage });
                }

                imageUri = fileUploadResult.FileUrl;
            }
            
            var game = new Game
            {
                Name = gameDto.Name,
                GenreId = gameDto.GenreId,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                Description = gameDto.Description,
                ImageUri = imageUri!
            };

            dbContext.Games.Add(game);

            await dbContext.SaveChangesAsync();

            logger.LogInformation(
                "Created game {GameName} with price {GamePrice}",
                game.Name,
                game.Price);

            return Results.CreatedAtRoute(
                EndpointNames.GetGame,
                new { id = game.Id },
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate,
                    game.Description,
                    game.ImageUri
                ));
        })
        .WithParameterValidation()
        .DisableAntiforgery();

    }
}

// Antiforgery tries to prevent CSRF attacks.

// CSRF stands for Cross-Site Request Forgery

// a type of attack where a malicious website tricks a user's browser
// into performing actions on another website without their consent.

// To achieve this, a malicious website will try to use the user's 
// authentication cookie, which browsers will send automatically,
// to try to make unauthorized requests to your website.

// Your API would process the request because it appears to be valid.

// For our application CSRF attacks are not a concern because:

// 1. CSRF exploits cookies sent by browsers with requests

// 2. Our API will authorize all requests using JWTs,
//      which come in an Authorization header, not cookies

// 3. The attacker cannot force the user's browser to send a 
//      custom Authorization header
