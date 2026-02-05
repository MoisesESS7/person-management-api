using PersonService.Application.Features.Persons.Queries.GetById;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.GetById
{
    public sealed class GetPersonByIdQueryHandlerTests
    {
        private readonly GetPersonByIdQueryFixture _fixture;
        private readonly GetPersonByIdQueryHandler _handler;

        public GetPersonByIdQueryHandlerTests()
        {
            _fixture = new GetPersonByIdQueryFixture();
            _handler = _fixture.CreateHandler();
        }
    }
}
