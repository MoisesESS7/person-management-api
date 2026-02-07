using FluentAssertions;
using Moq;
using PersonService.Application.Features.Persons.Commands.Create;
using PersonService.Domain.Entities;
using PersonService.Shared.Exceptions;
using PersonService.Shared.Results;
using PersonService.Tests.Common.Builders;

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

        [Fact]
        public async Task Handle_Should_Create_Person_When_Command_Is_Valid()
        {
            //Arrage
            var id = Guid.NewGuid().ToString();

            _fixture.RepositoryMock
                .Setup(r => r.AsQueryable())
                .Returns(Enumerable.Empty<Person>().AsQueryable());

            _fixture.RepositoryMock
                .Setup(r => r.CreateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person p, CancellationToken _) =>
                {
                    p.Id = id;
                    return p;
                });

            var command = new CreatePersonCommandBuilder().Build();

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(id);
            result.Value.Name.Should().Be(command.Name);
            result.Value.CpfNumber.Should().Be(command.CpfNumber);
            result.Value.RgNumber.Should().Be(command.RgNumber);

            _fixture.RepositoryMock
                .Verify(r => r.CreateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Fail_When_Name_Is_Duplicate()
        {
            //Arrage
            var existingPerson = new PersonBuilder()
                .WithName("John Doe")
                .Build();

            _fixture.RepositoryMock
                .Setup(r => r.AsQueryable())
                .Returns(new List<Person> { existingPerson }.AsQueryable());

            var command = new CreatePersonCommandBuilder()
                .WithName("John Doe")
                .Build();

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Should().HaveCountGreaterThan(0);

            result.Errors.Should()
                .Contain(e =>
                    e.Code == Errors.Person.DuplicateName.Code &&
                    e.Message == Errors.Person.DuplicateName.Message &&
                    e.Type == Errors.Person.DuplicateName.Type);

            _fixture.RepositoryMock
                .Verify(r => r.CreateAsync(
                        It.IsAny<Person>(),
                        It.IsAny<CancellationToken>()),
                        Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Fail_When_CpfNumber_Is_Duplicate()
        {
            //Arrage
            var existingPerson = new PersonBuilder()
                .WithName("Bob Brown")
                .WithCpfNumber("10987654321")
                .WithRgNumber("987654321")
                .Build();

            _fixture.RepositoryMock
                .Setup(r => r.AsQueryable())
                .Returns(new List<Person> { existingPerson }.AsQueryable());

            var command = new CreatePersonCommandBuilder()
                .WithCpfNumber("10987654321")
                .Build();

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Should().HaveCountGreaterThan(0);

            result.Errors.Should()
                .Contain(e =>
                    e.Code == Errors.Cpf.DuplicateNumber.Code &&
                    e.Message == Errors.Cpf.DuplicateNumber.Message &&
                    e.Type == Errors.Cpf.DuplicateNumber.Type);

            _fixture.RepositoryMock
                .Verify(r => r.CreateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()),
                    Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Fail_When_RgNumber_Is_Duplicate()
        {
            //Arrage
            var existingPerson = new PersonBuilder()
                .WithName("Bob Brown")
                .WithCpfNumber("10987654321")
                .WithRgNumber("987654321")
                .Build();

            _fixture.RepositoryMock
                .Setup(r => r.AsQueryable())
                .Returns(new List<Person> { existingPerson }.AsQueryable());

            var command = new CreatePersonCommandBuilder()
                .WithRgNumber("987654321")
                .Build();

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Should().HaveCountGreaterThan(0);

            result.Errors.Should()
                .Contain(e =>
                    e.Code == Errors.Rg.DuplicateNumber.Code &&
                    e.Message == Errors.Rg.DuplicateNumber.Message &&
                    e.Type == Errors.Rg.DuplicateNumber.Type);

            _fixture.RepositoryMock
                .Verify(r => r.CreateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()),
                    Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Throw_TechnicalException_When_Call_CreateAsync_Fail()
        {
            //Arrage
            _fixture.RepositoryMock
                .Setup(r => r.AsQueryable())
                .Returns(Enumerable.Empty<Person>().AsQueryable());

            _fixture.RepositoryMock
                .Setup(r => r.CreateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TechnicalException("Database unavailable"));

            var command = new CreatePersonCommandBuilder().Build();

            //Act
            Func<Task> act = async () =>
                await _handler.Handle(command, CancellationToken.None);

            //Assert
            var exception = await act.Should()
                .ThrowAsync<TechnicalException>();

            _fixture.RepositoryMock
                .Verify(r => r.CreateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }
    }
}
