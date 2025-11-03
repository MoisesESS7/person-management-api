using Infrastructure.Data.Extensions;
using MongoDB.Driver;
using Domain.Entities;

namespace Infrastructure.Data.Indexes
{
    public static class MongoIndexInitializer
    {
        public static void Configure(IMongoDatabase database)
        {
            CreatePersonIndex(database);
        }

        private static void CreatePersonIndex(IMongoDatabase database)
        {
            var databaseName = database.GetCollectionName<Person>();
            var collection = database.GetCollection<Person>(databaseName);

            var nameIndex = Builders<Person>.IndexKeys.Ascending(p => p.Name);
            var nameModel = new CreateIndexModel<Person>(nameIndex, new CreateIndexOptions
            {
                Unique = true,
                Name = "IDX_Person_Name"
            });

            var cpfIndex = Builders<Person>.IndexKeys.Ascending(p => p.Cpf);
            var cpfModel = new CreateIndexModel<Person>(cpfIndex, new CreateIndexOptions
            {
                Unique = true,
                Name = "IDX_Person_CPF"
            });

            var rgIndex = Builders<Person>.IndexKeys.Ascending(p => p.Rg);
            var rgModel = new CreateIndexModel<Person>(rgIndex, new CreateIndexOptions
            {
                Unique = true,
                Name = "IDX_Person_RG"
            });
            
            collection.Indexes.CreateMany([nameModel, cpfModel, rgModel]);
        }
    }
}
