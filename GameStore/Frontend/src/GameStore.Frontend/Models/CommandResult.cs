namespace GameStore.Frontend.Models;

public record CommandResult(bool Succeeded)
{
    public List<string> Errors { get; set; } = [];
}

