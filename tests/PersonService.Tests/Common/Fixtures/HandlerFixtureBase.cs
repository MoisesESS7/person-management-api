using Microsoft.Extensions.Logging;
using Moq;
using PersonService.Application.Interfaces.Repositories;
using PersonService.Domain.Entities;

namespace PersonService.Tests.Common.Fixtures
{
    public class HandlerFixtureBase<THandler>
    {
        public Mock<ILogger<THandler>> LoggerMock { get; }
        public Mock<IRepository<Person>> RepositoryMock { get; }

        public HandlerFixtureBase()
        {
            LoggerMock = new ();
            RepositoryMock = new ();
        }
    }
}
