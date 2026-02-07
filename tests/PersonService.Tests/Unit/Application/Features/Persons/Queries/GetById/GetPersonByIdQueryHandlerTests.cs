using FluentAssertions;
using Moq;
using PersonService.Application.Features.Persons.Queries.GetById;
using PersonService.Domain.Entities;
using PersonService.Shared.Exceptions;
using PersonService.Shared.Results;
using PersonService.Tests.Common.Builders;
using System.Linq.Expressions;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.GetById
{
    [Trait("Category", "Unit")]
    public sealed class GetPersonByIdQueryHandlerTests
    {
        private readonly GetPersonByIdQueryFixture _fixture;
        private readonly GetPersonByIdQueryHandler _handler;

        public GetPersonByIdQueryHandlerTests()
        {
            _fixture = new GetPersonByIdQueryFixture();
            _handler = _fixture.CreateHandler();
        }

        [Fact]
        public async Task Handle_Should_Return_Person_When_Person_Exists()
        {
            // Arrange
            var person = new PersonBuilder().Build();
            var query = new GetPersonByIdQuery(person.Id!);

            _fixture.RepositoryMock
                .Setup(r => r.GetAsync(
                    It.Is<Expression<Func<Person, bool>>>(expr =>
                        expr.Compile().Invoke(person)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(person);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(query.Id);
            result.Value.Name.Should().Be(person.Name);

            _fixture.RepositoryMock
                .Verify(r => r.GetAsync(
                    It.Is<Expression<Func<Person, bool>>>(expr =>
                        expr.Compile().Invoke(person)),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Fail_When_Person_Is_Not_Found()
        {
            // Arrange
            var query = new GetPersonByIdQuery(Guid.NewGuid().ToString());

            _fixture.RepositoryMock
                .Setup(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.Should()
                .Contain(
                    e => e.Code == Errors.Person.NotFound.Code &&
                    e.Message == Errors.Person.NotFound.Message &&
                    e.Type == Errors.Person.NotFound.Type);

            _fixture.RepositoryMock
                .Verify(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_TechnicalException_When_GetAsync_Throws()
        {
            // Arrange
            var query = new GetPersonByIdQuery(Guid.NewGuid().ToString());

            _fixture.RepositoryMock
                .Setup(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TechnicalException("Database unavailable"));

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<TechnicalException>();

            _fixture.RepositoryMock
                .Verify(r => r.GetAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }
    }
}
