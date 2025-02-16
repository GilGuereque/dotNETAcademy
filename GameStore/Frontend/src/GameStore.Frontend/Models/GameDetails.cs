using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models;

public class GameDetails
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The Genre field is required.")]
    public Guid? GenreId { get; set; }

    [Range(1, 100)]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
    
    [Required]
    [StringLength(500)]
    public required string Description { get; set; }
}