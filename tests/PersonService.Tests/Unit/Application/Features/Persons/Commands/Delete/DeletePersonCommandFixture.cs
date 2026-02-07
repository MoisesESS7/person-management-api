using PersonService.Application.Features.Persons.Commands.Delete;
using PersonService.Tests.Common.Fixtures;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Delete
{
    public sealed class DeletePersonCommandFixture : HandlerFixtureBase<DeletePersonCommandHandler>
    {
        public DeletePersonCommandHandler CreateHandler() => new (LoggerMock.Object, RepositoryMock.Object);
    }
}
