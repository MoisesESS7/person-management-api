namespace Application.Common.Models
{
    public sealed class PaginationMeta
    {
        public PageMeta? PageMeta { get; set; }
        public CursorMeta? CursorMeta { get; set; }

        public PaginationMeta(PageMeta? pageMeta)
        {
            PageMeta = pageMeta;
        }

        public PaginationMeta(CursorMeta? cursorMeta)
        {
            CursorMeta = cursorMeta;
        }
    }
}
