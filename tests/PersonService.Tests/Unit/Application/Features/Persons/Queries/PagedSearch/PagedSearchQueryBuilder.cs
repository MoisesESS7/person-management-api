using PersonService.Application.Common.Models;
using PersonService.Application.Features.Persons.Queries.PagedSearch;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.PagedSearch
{
    public sealed class PagedSearchQueryBuilder
    {
        private int _pageNumber = 1;
        private int _pageSize = 25;
        private string? _searchTerm = "John Doe";
        private string? _sortBy = "name";
        private bool _sortDescending;

        public PagedSearchQueryBuilder WithPageNumber(int pageNumber)
        {
            _pageNumber = pageNumber;
            return this;
        }

        public PagedSearchQueryBuilder WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public PagedSearchQueryBuilder WithSearchTerm(string? searchTerm)
        {
            _searchTerm = searchTerm;
            return this;
        }

        public PagedSearchQueryBuilder WithSortBy(string sortBy)
        {
            _sortBy = sortBy;
            return this;
        }

        public PagedSearchQueryBuilder WithSortDescending()
        {
            _sortDescending = true;
            return this;
        }

        public PagedSearchQuery Build()
        {
            return new PagedSearchQuery(new SearchParams
            {
                PageNumber = _pageNumber,
                PageSize = _pageSize,
                SearchTerm = _searchTerm,
                SortBy = _sortBy!,
                SortDescending = _sortDescending
            });
        }
    }
}
