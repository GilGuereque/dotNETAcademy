using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    // Refactor to invoke migrate DB and seed the DB with data
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        await app.MigrateDbAsync();
        await app.SeedDbAsync();
        app.Logger.LogInformation(23, "The database is ready!");
    }

    // Migrating the DB at the start of the app running
    private static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        GameStoreContext dbContext = scope.ServiceProvider
                                          .GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync(); // this method takes care of the .NET EF db update
    }

    // Pre-populates or Seeds the DB
    private static async Task SeedDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        GameStoreContext dbContext = scope.ServiceProvider
                                          .GetRequiredService<GameStoreContext>();
        // check to confirm if any values in Genres table
        if (!dbContext.Genres.Any())
        {
            dbContext.Genres.AddRange(
                new Genre { Name = "Fighting" },
                new Genre { Name = "Roleplaying" },
                new Genre { Name = "Massive Multiplayer Online" },
                new Genre { Name = "Kids and Family" },
                new Genre { Name = "Action Adventure" }
            );
            // Save changes made to DB
            await dbContext.SaveChangesAsync();
        }
    }
}
