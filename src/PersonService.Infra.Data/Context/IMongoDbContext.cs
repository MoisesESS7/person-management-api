using MongoDB.Driver;

namespace PersonService.Infra.Data.Context
{
    public interface IMongoDbContext
    {
        public IMongoCollection<T> GetCollection<T>();
    }
}
