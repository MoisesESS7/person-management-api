using PersonService.Application.Features.Persons.Commands.Delete;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Delete
{
    [Trait("Category", "Unit")]
    public sealed class DeletePersonCommandHandlerTests
    {
        private readonly DeletePersonCommandFixture _fixture;
        private readonly DeletePersonCommandHandler _handler;

        public DeletePersonCommandHandlerTests()
        {
            _fixture = new DeletePersonCommandFixture();
            _handler = _fixture.CreateHandler();
        }
    }
}
