using Infrastructure.Data.Extensions;
using Infrastructure.Data.Indexes;
using Infrastructure.Data.Persistence;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            MongoDbConventions.RegisterConventions();

            var connection = configuration["mongoDb:connectionString"];
            var databaseName = configuration["mongoDb:databaseName"];

            var client = new MongoClient(connection);

            _database = client.GetDatabase(databaseName);

            MongoIndexInitializer.Configure(_database);
        }
        
        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(_database.GetCollectionName<T>());
        }
    }
}
