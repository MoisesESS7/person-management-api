using MongoDB.Driver;

namespace Infrastructure.Data.Data.Context
{
    public interface IMongoDbContext
    {
        public IMongoCollection<T> GetCollection<T>();
    }
}
