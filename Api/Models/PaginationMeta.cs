namespace Api.Models
{
    public sealed class PaginationMeta
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public long TotalRecords { get; private set; }
        public int TotalPages { get; private set; }

        public PaginationMeta(int pageNumber, int pageSize, long totalRecords, int totalPages)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = totalPages;
        }
    }
}
