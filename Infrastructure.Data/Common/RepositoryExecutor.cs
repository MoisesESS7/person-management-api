using System.Runtime.CompilerServices;
using MongoDB.Driver;
using Infrastructure.Data.Exceptions;
using Polly;
using Polly.Retry;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;

namespace Infrastructure.Data.Common
{
    public class RepositoryExecutor : IRepositoryExecutor
    {
        private readonly ILogger<RepositoryExecutor> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        public RepositoryExecutor(ILogger<RepositoryExecutor> logger)
        {
            _logger = logger;

            _retryPolicy = Policy
                .Handle<MongoException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(200 * Math.Pow(2, attempt)),
                    onRetry: (exception, delay, attempt, context) =>
                    {
                        _logger.LogWarning(
                            exception,
                            "Retry {Attempt} after {Delay}ms due to {ExceptionType}. Operation: {Operation}",
                            attempt,
                            delay.TotalMilliseconds,
                            exception.GetType().Name,
                            context.OperationKey ?? "N/A"
                        );
                    }
                );
        }

        public async Task ExecuteAsync(Func<Task> operation, [CallerMemberName] string? operationName = null)
        {
            await ExecuteAsync(async () =>
            {
                await operation();
                return true;
            }, operationName);
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> operation, [CallerMemberName] string? operationName = null)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async (context) =>
                    {
                        return await operation();
                    },
                    new Context(operationName)
                );
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error in operation {Operation}", operationName);

                if (ex is MongoConnectionException)
                    throw new DatabaseConnectionException("Failed to connect to MongoDB.", ex);

                if (ex is MongoExecutionTimeoutException)
                    throw new DatabaseTimeoutException("The MongoDB operation timed out.", ex);

                if (ex is MongoWriteException || ex is MongoWriteConcernException)
                    throw new DatabaseWriteException("The MongoDB operation write failed.", ex);

                throw new InfrastructureLayerException("A MongoDB error occurred.", ex);
            }
            catch (TimeoutException ex)
            {
                _logger.LogWarning(ex, "Timeout in operation {Operation}", operationName);
                throw new DatabaseTimeoutException("The database operation timed out.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in operation {Operation}", operationName);
                throw new InfrastructureLayerException("Unexpected error in repository layer.", ex);
            }
        }        
    }
}
