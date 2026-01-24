using System.Runtime.CompilerServices;

namespace PersonService.Infra.Data.Common
{
    public interface IRepositoryExecutor
    {
        Task ExecuteAsync(Func<Task> operation, [CallerMemberName] string? operationName = null);
        Task<T> ExecuteAsync<T>(Func<Task<T>> operation, [CallerMemberName] string? operationName = null);
    }
}
