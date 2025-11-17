namespace Application.Common.Models
{
    public sealed class SearchParams
    {
        private int _pageNumber = 1;
        private int _pageSize = 25;

        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                _pageNumber = value < _pageNumber ? _pageNumber : value;
            }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _pageSize || value < 1) ? _pageSize : value; }
        }

        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; }
    }
}
