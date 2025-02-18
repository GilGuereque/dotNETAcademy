using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using GameStore.Api.Shared.ErrorHandling;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(); // Add support for ProblemDetails responses (RFC 7807)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // Add global exception handler

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString); //simpler way for connString

// Registers services to be used later in the pipeline (middleware)
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration; // Log all fields from the request and response
    options.CombineLogs = true; // Combine logs from the request and response into a single log entry
});

builder.Services.AddEndpointsApiExplorer(); // Services support the generation of documents using OpenAPI
builder.Services.AddSwaggerGen(); // Building services that enable us to use OpenAPI support

builder.Services.AddHttpContextAccessor()
                .AddSingleton<FileUploader>();

var app = builder.Build();

app.UseStaticFiles(); // Serve the static files in wwwroot folder

// All games related endpoints
app.MapGames();
// Genres related endpoints
app.MapGenres();

// Middleware:
if (app.Environment.IsDevelopment()) // Only run in development environment
{
    app.UseSwagger(); // Use Swagger OpenAPI JSON document generation service
    app.UseSwaggerUI(); // Use Swagger interactice UI service for testing endpoints - http://localhost:5134/swagger/index.html (use backend PORT)
}
else
{
    app.UseExceptionHandler(); // Handle exceptions and return a 500 response
}
// app.UseMiddleware<RequestTimingMiddleware>(); // Logs the time to process the request and response back to the client
app.UseHttpLogging(); // Logs all HTTP requests and responses

app.UseStatusCodePages(); // Return a status code page for 404, 500, etc. responses

// Migrate & seed the DB
await app.InitializeDbAsync();

app.Run();


/* NOTES:
What service lifetime to use for a dbContext?
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