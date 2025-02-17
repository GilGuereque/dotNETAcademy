using System.Diagnostics;
using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using GameStore.Api.Shared.Timing;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString); //simpler way for connString

/* What service lifetime to use for a dbContext?
- DbContext is designed to be used as a single Unit of Work
- DbContext created ---> entity changes tracked -> save changes -> dispose dbContext
- DB connections are expensive - Don't want to keep them open all the time.
- DbContet is not thread safe (not very to receive calls from multiple threads
    at the same time.)
- Increased memory usage due to change tracking

USE: Scoped service lifetime
- Aligning the context lifetime to the lifetime of the request
- When a request comes in, the context is created, and disposed once request ends
- Thread safe to access the dbContext 
- There is only one thread executing each client request at a given time
- Ensure each request gets a separate DbContext instance
*/

// You must register the services before you build constructing the app
// TODO: Delete Data/GameStoreData.cs & Data/GameStoreLogger.cs files (no longer needed)

var app = builder.Build();

// All games related endpoints
app.MapGames();
// Genres related endpoints
app.MapGenres();
// Middleware to log the request time to process the request and response back to the client
app.UseMiddleware<RequestTimingMiddleware>();

// Migrate & seed the DB
await app.InitializeDbAsync();

app.Run();