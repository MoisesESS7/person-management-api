using PersonService.Application.Features.Persons.Commands.Create;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Create
{
    [Trait("Class", "Unit")]
    public sealed class CreatePersonCommandHandlerTests
    {
        private readonly CreatePersonCommandFixture _fixture;
        private readonly CreatePersonCommandHandler _handler;

        public CreatePersonCommandHandlerTests()
        {
            _fixture = new CreatePersonCommandFixture();
            _handler = _fixture.CreateHandler();
        }
    }
}
