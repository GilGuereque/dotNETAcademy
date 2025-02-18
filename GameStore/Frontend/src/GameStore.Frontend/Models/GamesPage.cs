namespace GameStore.Frontend.Models;

public record class GamesPage(int TotalPages, IEnumerable<GameSummary> Data);
