using System.Linq.Expressions;

namespace PersonService.Application.Interfaces.Repositories
{
    public interface IWriteOnlyRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    }
}
