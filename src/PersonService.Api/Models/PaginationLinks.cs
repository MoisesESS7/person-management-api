namespace PersonService.Api.Models
{
    public sealed class PaginationLinks
    {
        public string Self { get; set; } = string.Empty;
        public string? First { get; set; }
        public string? Prev { get; set; }
        public string? Next { get; set; }
        public string? Last { get; set; }
    }
}
