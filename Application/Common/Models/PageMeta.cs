using Application.Exceptions;

namespace Application.Common.Models
{
    public sealed class PageMeta
    {
        private int _pageNumber;

        public long TotalRecords { get; private set; }
        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);
        public int PageSize { get; private set; }
        public int PageNumber
        {
            get { return _pageNumber; }
            private set
            {
                if (value > TotalPages)
                    throw new PageNotFoundException("The requested page does not exist.");

                _pageNumber = value;
            }
        }

        public PageMeta(int pageNumber, int pageSize, long totalRecords)
        {
            TotalRecords = totalRecords;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
