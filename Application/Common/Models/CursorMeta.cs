namespace Application.Common.Models
{
    public sealed class CursorMeta
    {
        public string? NextCursor { get; private set; }
        public string? PreviousCursor { get; private set; }

        public CursorMeta(string? nextCursor, string? previousCursor)
        {
            NextCursor = nextCursor;
            PreviousCursor = previousCursor;
        }
    }
}
