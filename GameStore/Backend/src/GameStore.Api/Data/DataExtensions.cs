using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    // Refactor to invoke migrate DB and seed the DB with data
    public static void InitializeDb(this WebApplication app)
    {
        app.MigrateDb();
        app.SeedDb();
    }

    // Migrating the DB at the start of the app running
    private static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        GameStoreContext dbContext = scope.ServiceProvider
                                          .GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate(); // this method takes care of the .NET EF db update
    }

    // Pre-populates or Seeds the DB
    private static void SeedDb(this WebApplication app)
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
            dbContext.SaveChanges();
        }
    }
}
