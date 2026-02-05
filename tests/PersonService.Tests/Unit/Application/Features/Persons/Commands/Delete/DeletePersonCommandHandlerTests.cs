using FluentAssertions;
using Moq;
using PersonService.Application.Features.Persons.Commands.Delete;
using PersonService.Domain.Entities;
using PersonService.Shared.Exceptions;
using PersonService.Shared.Results;
using System.Linq.Expressions;

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

        [Fact]
        public async Task Handle_Should_Delete_Person_When_Person_Exists()
        {
            // Arrange
            var command = new DeletePersonCommand(Guid.NewGuid().ToString());

            _fixture.RepositoryMock
                .Setup(r => r.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _fixture.RepositoryMock
                .Setup(r => r.DeleteAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();

            _fixture.RepositoryMock.Verify(r => r.ExistsAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);

            _fixture.RepositoryMock.Verify(r => r.DeleteAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Fail_When_Person_Not_Found()
        {
            // Arrange
            var command = new DeletePersonCommand(Guid.NewGuid().ToString());

            _fixture.RepositoryMock
                .Setup(repo => repo.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Should()
                .Contain(
                    e => e.Code == Errors.Person.NotFound.Code &&
                    e.Message == Errors.Person.NotFound.Message &&
                    e.Type == Errors.Person.NotFound.Type
                );

            _fixture.RepositoryMock.Verify(r => r.ExistsAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);

            _fixture.RepositoryMock.Verify(r => r.DeleteAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Throw_TechnicalException_When_Call_ExistsAsync_Throws()
        {
            // Arrange
            var command = new DeletePersonCommand(Guid.NewGuid().ToString());

            _fixture.RepositoryMock
                .Setup(repo => repo.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TechnicalException("Database unavailable"));

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<TechnicalException>();

            _fixture.RepositoryMock.Verify(r => r.ExistsAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);

            _fixture.RepositoryMock.Verify(r => r.DeleteAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Throw_TechnicalException_When_Call_DeleteAsync_Throws()
        {
            // Arrange
            var command = new DeletePersonCommand(Guid.NewGuid().ToString());

            _fixture.RepositoryMock
                .Setup(repo => repo.ExistsAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _fixture.RepositoryMock
                .Setup(r => r.DeleteAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TechnicalException("Database unavailable"));

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<TechnicalException>();

            _fixture.RepositoryMock.Verify(r => r.ExistsAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);

            _fixture.RepositoryMock.Verify(r => r.DeleteAsync(
                It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
