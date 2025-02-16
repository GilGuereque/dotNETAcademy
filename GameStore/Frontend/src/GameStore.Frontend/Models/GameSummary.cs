namespace GameStore.Frontend.Models;

public class GameSummary
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Genre { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}