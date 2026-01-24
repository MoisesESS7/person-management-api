using PersonService.Application.Common.Models;
using PersonService.Application.Features.Persons.Responses;
using MediatR;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Queries.PagedSearch
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
