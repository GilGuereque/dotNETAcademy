using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace GameStore.Api.Shared.ErrorHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.TraceId;

        logger.LogError(
            exception,
            "Could not process a request on machine {Machine}. TraceId: {TraceId}",
            Environment.MachineName,
            traceId);

        await Results.Problem(
            title: "An error ocurred while  processing your request.",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, object?>
            {
                {"traceId", traceId.ToString()}
            }
        ).ExecuteAsync(httpContext);

        return true;
    }
}