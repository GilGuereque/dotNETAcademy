using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient(HttpClient httpClient)
{
    private readonly List<string> defaultDetail = ["Unknown error."];

    public async Task<GameSummary[]> GetGamesAsync()
        => await httpClient.GetFromJsonAsync<GameSummary[]>($"games") ?? [];

    public async Task<CommandResult> AddGameAsync(GameDetails game)
    {
        var response = await httpClient.PostAsJsonAsync("games", game);
        return await response.HandleAsync();
    }

    public async Task<GameDetails> GetGameAsync(Guid id)
        => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}")
            ?? throw new Exception("Could not find game!");

    public async Task<CommandResult> UpdateGameAsync(GameDetails updatedGame)
    {
        var response = await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);
        return await response.HandleAsync();
    }

    public async Task<CommandResult> DeleteGameAsync(Guid id)
    {
        var response = await httpClient.DeleteAsync($"games/{id}");
        return await response.HandleAsync();
    }
}
