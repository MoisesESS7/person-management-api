using FluentAssertions;
using Moq;
using PersonService.Application.Features.Persons.Queries.GetById;
using PersonService.Domain.Entities;
using PersonService.Tests.Common.Builders;
using System.Linq.Expressions;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.GetById
{
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
    }
}
