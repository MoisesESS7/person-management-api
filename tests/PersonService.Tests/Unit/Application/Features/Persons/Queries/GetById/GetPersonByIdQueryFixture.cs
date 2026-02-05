using PersonService.Application.Features.Persons.Queries.GetById;
using PersonService.Tests.Common.Fixtures;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.GetById
{
    public sealed class GetPersonByIdQueryFixture : HandlerFixtureBase<GetPersonByIdQueryHandler>
    {
        public GetPersonByIdQueryHandler CreateHandler() => new(LoggerMock.Object, RepositoryMock.Object);
    }
}
