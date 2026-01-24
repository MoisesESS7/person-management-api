using PersonService.Application.Common.Models;
using PersonService.Application.Interfaces.Repositories;
using PersonService.Domain.Entities;
using PersonService.Infra.Data.Common;
using PersonService.Infra.Data.Context;
using PersonService.Infra.Data.Exceptions;
using PersonService.Infra.Data.Extensions;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace PersonService.Infra.Data.Repositories
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

        public async Task<IEnumerable<TEntity>> SeachPagedAsync(SearchParams searchParams, CancellationToken cancellationToken = default)
        {
            var filter = string.IsNullOrWhiteSpace(searchParams.SearchTerm)
                ? Builders<TEntity>.Filter.Empty
                : Builders<TEntity>.Filter.Text(searchParams.SearchTerm);

            var skip = (searchParams.PageNumber - 1) * searchParams.PageSize;

            var items = await _collection.Find(filter)
                                   .ApplyOrdering(searchParams.SortBy, searchParams.SortDescending)
                                   .Skip(skip)
                                   .Limit(searchParams.PageSize)                                
                                   .ToListAsync(cancellationToken);

            return items;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _executor.ExecuteAsync(() =>
                      _collection.InsertOneAsync(entity, cancellationToken: cancellationToken));

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
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
