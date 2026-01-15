using Application.Common.Models;
using Application.Features.Persons.Responses;
using MediatR;
using Shared.Results;

namespace Application.Features.Persons.Queries.PagedSearch
{
    public sealed class PagedSearchQuery : IRequest<ResultOfT<PagedResult<PersonResponse>>>
    {
        public SearchParams SearchParams { get; private set; }

        public PagedSearchQuery(SearchParams searchParams)
        {
            SearchParams = searchParams;
        }
    }
}
