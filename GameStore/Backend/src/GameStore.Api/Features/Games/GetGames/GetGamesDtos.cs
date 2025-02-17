namespace GameStore.Api.Features.Games.GetGames;

public record GetGamesDto(
    int PageNumber = 1,
    int PageSize = 5,
    string? Name = null);

public record GamesPageDto(int TotalPages, IEnumerable<GameSummaryDto> Data);

public record GameSummaryDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate,
    string ImageUri
);
