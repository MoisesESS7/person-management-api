namespace PersonService.Application.Common.Models
{
    public sealed class SearchParams
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? SearchTerm { get; init; }
        public string SortBy { get; init; } = "Name";
        public bool SortDescending { get; init; }
    }
}
