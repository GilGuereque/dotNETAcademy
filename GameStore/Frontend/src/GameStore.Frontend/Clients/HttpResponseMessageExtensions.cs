using System.Text.Json;
using GameStore.Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Frontend.Clients;

public static class HttpResponseMessageExtensions
{
    private static readonly List<string> defaultDetail = ["Unknown error."];

    public static async Task<CommandResult> HandleAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return new CommandResult(true);
        }

        // Get as many error details as possible
        var responseContent = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(responseContent))
        {
            return new CommandResult(false) { Errors = defaultDetail };
        }

        if (response.Content.Headers.ContentType?.MediaType != "application/problem+json")
        {
            return new CommandResult(false) { Errors = [responseContent] };
        }

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent);
        List<string> errors = [];

        if (!string.IsNullOrEmpty(problemDetails?.Detail))
        {
            errors.Add(problemDetails.Detail);
        }
        else if (!string.IsNullOrEmpty(problemDetails?.Title))
        {
            errors.Add(problemDetails.Title);
        }

        if (problemDetails?.Extensions.TryGetValue("errors", out var value) == true && value is JsonElement errorsElement)
        {
            foreach (var errorEntry in errorsElement.EnumerateObject())
            {
                errors.AddRange(
                    errorEntry.Value.EnumerateArray().Select(
                        e => e.GetString() ?? string.Empty)
                    .Where(e => !string.IsNullOrEmpty(e)));
            }
        }

        // return the error list
        return new CommandResult(false) { Errors = errors.Count == 0 ? defaultDetail : errors };
    }
}