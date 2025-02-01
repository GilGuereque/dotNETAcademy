using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GameStore.Api.Models;

public class Game
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; } //? makes the string able to be null, otherwise you would assign it an empty string

    public required Genre Genre { get; set; }

    [Required]
    [Range(1, 100)]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; } // we only care about the specific date

    public required string Description { get; set; }
}
