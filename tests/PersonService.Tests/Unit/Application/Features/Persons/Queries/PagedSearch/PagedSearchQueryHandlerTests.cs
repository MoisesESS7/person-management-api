using PersonService.Application.Features.Persons.Queries.PagedSearch;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.PagedSearch
{
    public sealed class PagedSearchQueryHandlerTests
    {
        private readonly PagedSearchQueryFixture _fixture;
        private readonly PagedSearchQueryHandler _handler;

        public PagedSearchQueryHandlerTests()
        {
            _fixture = new PagedSearchQueryFixture();
            _handler = _fixture.CreateHandler();
        }
    }
}
