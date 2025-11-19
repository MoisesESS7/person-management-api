using MongoDB.Driver;

namespace Infrastructure.Data.Context
{
    public interface IMongoDbContext
    {
        public IMongoCollection<T> GetCollection<T>();
    }
}
