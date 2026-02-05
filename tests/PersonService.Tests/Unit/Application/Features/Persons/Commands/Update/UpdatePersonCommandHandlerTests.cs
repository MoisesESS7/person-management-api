using FluentAssertions;
using Moq;
using PersonService.Application.Features.Persons.Commands.Update;
using PersonService.Domain.Entities;
using PersonService.Shared.Exceptions;
using PersonService.Shared.Results;
using PersonService.Tests.Common.Builders;
using System.Linq.Expressions;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Update
{
    [Trait("Class", "Unit")]
    public sealed class UpdatePersonCommandHandlerTests
    {
        private readonly UpdatePersonCommandFixture _fixture;
        private readonly UpdatePersonCommandHandler _handler;

        public UpdatePersonCommandHandlerTests()
        {
            _fixture = new UpdatePersonCommandFixture();
            _handler = _fixture.CreateHandler();
        }

        [Fact]
        public async Task Handle_Should_Update_Person_Name_When_Name_Is_Not_Duplicate()
        {
            // Arrange
            Person? updatedPerson = null;

            var command = new UpdatePersonCommandBuilder().Build();

            var person = new PersonBuilder()
                .WithName(command.Name)
                .Build();

            _fixture.RepositoryMock
                .Setup(r => r.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _fixture.RepositoryMock
                .Setup(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(person);

            _fixture.RepositoryMock
                .Setup(r => r.UpdateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()))
                .Callback<Person, CancellationToken>((p, ct) => updatedPerson = p)
                .ReturnsAsync((Person p, CancellationToken ct) => p);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(person.Id);
            result.Value.Name.Should().Be(command.Name);

            updatedPerson!.Id.Should().Be(person.Id);
            updatedPerson.Name.Should().Be(command.Name);

            _fixture.RepositoryMock
                .Verify(r => r.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.UpdateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Fail_When_Name_Is_Duplicate()
        {
            // Arrange
            var command = new UpdatePersonCommandBuilder().Build();

            var person = new PersonBuilder()
                .WithName(command.Name)
                .Build();

            _fixture.RepositoryMock
                .Setup(r => r.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Value.Should().BeNull();
            result.Errors.Should()
                .Contain(
                    e => e.Code == Errors.Person.DuplicateName.Code &&
                    e.Message == Errors.Person.DuplicateName.Message &&
                    e.Type == ErrorType.Conflict);

            _fixture.RepositoryMock
                .Verify(r => r.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Never);

            _fixture.RepositoryMock
                .Verify(r => r.UpdateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()),
                    Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Return_Fail_When_Person_Not_Found()
        {
            // Arrange
            var command = new UpdatePersonCommandBuilder().Build();

            var person = new PersonBuilder()
                .WithName(command.Name)
                .Build();

            _fixture.RepositoryMock
                .Setup(r => r.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _fixture.RepositoryMock
                .Setup(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Value.Should().BeNull();
            result.Errors.Should()
                .Contain(e =>
                    e.Code == Errors.Person.NotFound.Code &&
                    e.Message == Errors.Person.NotFound.Message &&
                    e.Type == ErrorType.NotFound);

            _fixture.RepositoryMock
                .Verify(r => r.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.UpdateAsync(
                    It.IsAny<Person>(),
                    It.IsAny<CancellationToken>()),
                    Times.Never);
        }
    }
}
