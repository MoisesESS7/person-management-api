using FluentAssertions;
using Moq;
using PersonService.Application.Common.Models;
using PersonService.Application.Features.Persons.Queries.PagedSearch;
using PersonService.Domain.Entities;
using PersonService.Shared.Exceptions;
using PersonService.Tests.Common.Builders;
using System.Linq.Expressions;

namespace PersonService.Tests.Unit.Application.Features.Persons.Queries.PagedSearch
{
    [Trait("Category", "Unit")]
    public sealed class PagedSearchQueryHandlerTests
    {
        private readonly PagedSearchQueryFixture _fixture;
        private readonly PagedSearchQueryHandler _handler;

        public PagedSearchQueryHandlerTests()
        {
            _fixture = new PagedSearchQueryFixture();
            _handler = _fixture.CreateHandler();
        }

        [Fact]
        public async Task Handle_Should_Return_PagedResult_When_Persons_Exist()
        {
            // Arrange
            var query = new PagedSearchQueryBuilder()
                .WithPageNumber(1)
                .WithPageSize(2)
                .Build();

            var persons = new List<Person>
            {
                new PersonBuilder().Build(),
                new PersonBuilder()
                .WithName("Bob Brown")
                .WithCpfNumber("98765432101")
                .WithRgNumber("987654321")
                .Build()
            };

            _fixture.RepositoryMock
                .Setup(repo => repo.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(persons.Count);

            _fixture.RepositoryMock
                .Setup(repo => repo.SeachPagedAsync(
                    It.IsAny<SearchParams>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(persons);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value?.Data.Should().HaveCount(persons.Count);
            result.Value?.Meta.PageMeta?.TotalRecords.Should().Be(persons.Count);
            result.Value?.Meta.PageMeta?.PageNumber.Should().Be(query.SearchParams.PageNumber);
            result.Value?.Meta.PageMeta?.PageSize.Should().Be(query.SearchParams.PageSize);
            result.Value?.Meta.PageMeta?.TotalPages.Should().Be(1);

            _fixture.RepositoryMock
                .Verify(r => r.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.SeachPagedAsync(
                    It.Is<SearchParams>(p =>
                        p.SortBy == query.SearchParams.SortBy &&
                        p.SortDescending == query.SearchParams.SortDescending &&
                        p.SearchTerm == query.SearchParams.SearchTerm
                    ),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_PagedResult_When_No_Persons_Exist()
        {
            // Arrange
            var query = new PagedSearchQueryBuilder()
                .WithPageNumber(1)
                .WithPageSize(2)
                .Build();

            _fixture.RepositoryMock
                .Setup(repo => repo.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value?.Data.Should().BeEmpty();
            result.Value?.Meta.PageMeta?.PageNumber.Should().Be(query.SearchParams.PageNumber);
            result.Value?.Meta.PageMeta?.PageSize.Should().Be(query.SearchParams.PageSize);
            result.Value?.Meta.PageMeta?.TotalPages.Should().Be(0);
            result.Value?.Meta.PageMeta?.TotalRecords.Should().Be(0);

            _fixture.RepositoryMock
                .Verify(r => r.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.SeachPagedAsync(
                    It.IsAny<SearchParams>(),
                    It.IsAny<CancellationToken>()),
                    Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Throw_TechnicalException_When_Call_CountAsync_Throws()
        {
            // Arrange
            var query = new PagedSearchQueryBuilder()
                .WithPageNumber(1)
                .WithPageSize(2)
                .Build();

            _fixture.RepositoryMock
                .Setup(repo => repo.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TechnicalException("Database unavailable"));

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<TechnicalException>();

            _fixture.RepositoryMock
                .Verify(r => r.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.SeachPagedAsync(
                    It.IsAny<SearchParams>(),
                    It.IsAny<CancellationToken>()),
                    Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Throw_TechnicalException_When_Call_SeachPagedAsync_Throws()
        {
            // Arrange
            var query = new PagedSearchQueryBuilder()
                .WithPageNumber(1)
                .WithPageSize(2)
                .Build();

            _fixture.RepositoryMock
                .Setup(repo => repo.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _fixture.RepositoryMock
                .Setup(repo => repo.SeachPagedAsync(
                    It.IsAny<SearchParams>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TechnicalException("Database unavailable"));

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<TechnicalException>();

            _fixture.RepositoryMock
                .Verify(r => r.CountAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);

            _fixture.RepositoryMock
                .Verify(r => r.SeachPagedAsync(
                    It.IsAny<SearchParams>(),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
        }
    }
}
