namespace PersonService.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>, IWriteOnlyRepository<TEntity> {}
}
