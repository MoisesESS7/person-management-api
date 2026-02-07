using PersonService.Application.Features.Persons.Commands.Update;
using PersonService.Tests.Common.Fixtures;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Update
{
    public sealed class UpdatePersonCommandFixture : HandlerFixtureBase<UpdatePersonCommandHandler>
    {
        public UpdatePersonCommandHandler CreateHandler() => new (LoggerMock.Object, RepositoryMock.Object);
    }
}
