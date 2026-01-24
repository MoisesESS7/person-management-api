using Microsoft.AspNetCore.Mvc;

namespace PersonService.Api.Models
{
    public sealed class SearchParamsQuery
    {
        [FromQuery(Name = "page")]
        public int PageNumber { get; set; }

        [FromQuery(Name = "page-size")]
        public int PageSize { get; set; }

        [FromQuery(Name = "search-term")]
        public string? SearchTerm { get; set; }

        [FromQuery(Name = "sort-by")]
        public string? SortBy { get; set; }

        [FromQuery(Name = "sort-descending")]
        public bool SortDescending { get; set; }
    }
}
