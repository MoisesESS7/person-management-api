using Application.Common.Models;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IReadOnlyRepository<TEntity>
    {
        IQueryable<TEntity> AsQueryable();
        Task<IEnumerable<TEntity>> SeachPagedAsync(SearchParams searchParams, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<long> CountAsync(Expression<Func<TEntity, bool>>? expression = default, CancellationToken cancellationToken = default);
    }
}
