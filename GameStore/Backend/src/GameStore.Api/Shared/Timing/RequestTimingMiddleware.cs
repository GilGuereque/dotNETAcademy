using System.Diagnostics;

namespace GameStore.Api.Shared.Timing;

public class RequestTimingMiddleware(
    RequestDelegate next,
    ILogger<RequestTimingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();

        try
        {
            stopwatch.Start();

            await next(context); // call the next middleware in the request pipeline
        }
        finally
        {
            stopwatch.Stop(); // stop the stopwatch once the response is sent back to the client

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds; // get the elapsed time in milliseconds
            logger.LogInformation(
                "{Requestmethod} {RequestPath} executed with status {Status} in {ElapsedMilliseconds}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsedMilliseconds); // log the elapsed time
        }
    }
}
