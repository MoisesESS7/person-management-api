using MongoDB.Driver;
using Domain.Attributes;

namespace Infrastructure.Data.Extensions
{
    public static class MongoExtensions
    {
        public static string GetCollectionName<T>(this IMongoDatabase mongoDatabase)
        {
            var attribute = (BsonCollectionAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(BsonCollectionAttribute));

            return attribute == null
                ? throw new InvalidOperationException($"The entity { typeof(T).Name } is missing the attribute[BsonCollection].")
                : attribute.Name;
        }
    }
}
