using PersonService.Application.Features.Persons.Commands.Update;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Update
{
    [Trait("Class", "Unit")]
    public sealed class UpdatePersonCommandHandlerTests
    {
        private readonly UpdatePersonCommandFixture _fixturer;
        private readonly UpdatePersonCommandHandler _handler;

        public UpdatePersonCommandHandlerTests()
        {
            _fixturer = new UpdatePersonCommandFixture();
            _handler = _fixturer.CreateHandler();
        }
    }
}
