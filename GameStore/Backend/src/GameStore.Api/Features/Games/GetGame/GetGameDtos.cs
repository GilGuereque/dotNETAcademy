namespace GameStore.Api.Features.Games.GetGame;

public record GameDetailsDto(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description,
    string ImageUri
);