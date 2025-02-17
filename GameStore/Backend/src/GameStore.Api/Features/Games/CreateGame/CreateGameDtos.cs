using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Features.Games.CreateGame;

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
)
{
    public IFormFile? ImageFile { get; set; }
};

public record GameDetailsDto(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description,
    string ImageUri
);