using PersonService.Application.Features.Persons.Queries.PagedSearch;
using PersonService.Tests.Common.Fixtures;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.PagedSearch
{
    public sealed class PagedSearchQueryFixture : HandlerFixtureBase<PagedSearchQueryHandler>
    {
        public PagedSearchQueryHandler CreateHandler() => new (LoggerMock.Object, RepositoryMock.Object);
    }
}
