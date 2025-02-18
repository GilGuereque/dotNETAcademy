namespace GameStore.Frontend.Models;

public record class PaginationInfo(int CurrentPage, int TotalPages, string? NameSearch = null)
{
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}