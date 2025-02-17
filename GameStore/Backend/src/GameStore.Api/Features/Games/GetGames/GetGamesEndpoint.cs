using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app)
    {
        // GET /games (retrieve all existing games)
        app.MapGet("/", async (
            GameStoreContext dbContext,
            [AsParameters] GetGamesDto request) =>
        {
            var skipcount = (request.PageNumber - 1) * request.PageSize;
            // skipcount = (1 - 1) * 5 = 0 (skip 0 records and get first page)
            // skipcount = (2 - 1) * 5 = 5 (skip 5 records and get second page)

            var filteredGames = dbContext.Games
                                .Where(game => string.IsNullOrWhiteSpace(request.Name)
                                        || game.Name.Contains(request.Name)); // Filter games by name (if provided)


            var gamesOnPage = await filteredGames // Get the games on the current page (page number and page size)
                                .OrderBy(game => game.Name) // Order by name (ascending)
                                .Skip(skipcount) // Skip the first n records (n = skipcount) and get the next page of records
                                .Take(request.PageSize) // Take the next n records (n = request.PageSize) and get the next page of records
                                .Include(game => game.Genre)
                                .Select(game => new GameSummaryDto(
                                    game.Id,
                                    game.Name,
                                    game.Genre!.Name,
                                    game.Price,
                                    game.ReleaseDate
                                ))
                                .AsNoTracking()
                                .ToListAsync();

            var totalGames = await filteredGames.CountAsync(); // Get the total number of games in the database
            var totalPages = (int)Math.Ceiling(totalGames / (double)request.PageSize); // Get the total number of pages (totalGames / PageSize)
            // 23 / 10 = 2.3 -> 3 pages (Math.Ceiling rounds up to the nearest whole number)
            return new GamesPageDto(totalPages, gamesOnPage); // Return the total number of pages and the games on the current page
        });
    }
}
