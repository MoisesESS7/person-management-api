using PersonService.Application.Features.Persons.Commands.Create;
using PersonService.Tests.Common.Fixtures;


namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Create
{
    public sealed class CreatePersonCommandFixture : HandlerFixtureBase<CreatePersonCommandHandler>
    {
        public CreatePersonCommandHandler CreateHandler() => new (LoggerMock.Object, RepositoryMock.Object);
    }
}
