using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Data.Data.Context;
using Infrastructure.Data.Exceptions;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Entity<string>
    {
        private readonly IRepositoryExecutor _executor;
        private readonly IMongoCollection<TEntity> _collection;

        public GenericRepository(IMongoDbContext mongoContext, IRepositoryExecutor executor)
        {
            _collection = mongoContext.GetCollection<TEntity>();
            _executor = executor;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _collection.AsQueryable();    
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(() => 
                                    _collection
                                        .Find(expression)
                                        .FirstOrDefaultAsync(cancellationToken));

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(() => 
                                    _collection
                                        .Find(_ => true)
                                        .ToListAsync(cancellationToken));

            return result;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _executor.ExecuteAsync(() =>
                      _collection.InsertOneAsync(entity, cancellationToken: cancellationToken));

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if(entity is null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            var result = await _executor.ExecuteAsync(() => 
                                    _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken));

            if (result.ModifiedCount <= 0)
                throw new DatabaseWriteException($"No documents were updated for entity {typeof(TEntity).Name} with ID {entity.Id}.");

            return entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            var result = await _executor.ExecuteAsync(() => 
                                    _collection.DeleteOneAsync(expression, cancellationToken));

            if (result.DeletedCount <= 0)
                throw new DatabaseWriteException("Failed to delete entity in MongoDB.");

            return true;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _executor.ExecuteAsync(() =>
                             _collection
                                .Find(expression)
                                .AnyAsync(cancellationToken));
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? expression = default, CancellationToken cancellationToken = default)
        {
            return await _executor.ExecuteAsync(() =>
                            _collection.CountDocumentsAsync(expression ?? (_ => true), cancellationToken: cancellationToken));
        }
    }
}
