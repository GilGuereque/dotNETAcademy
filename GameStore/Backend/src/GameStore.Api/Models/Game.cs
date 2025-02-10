namespace GameStore.Api.Models;

public class Game
{
    public Guid Id { get; set; }

    public required string Name { get; set; } //? makes the string able to be null, otherwise you would assign it an empty string

    public required Genre? Genre { get; set; }

    public Guid GenreId { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; } // we only care about the specific date

    public required string Description { get; set; }
}
