using MongoDB.Driver;
using System.Reflection;

namespace PersonService.Infra.Data.Extensions
{
    public static class MongoQueryableExtensions
    {
        public static IFindFluent<T, T> ApplyOrdering<T>(
            this IFindFluent<T, T> query,
            string? sortBy,
            bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query;

            var property = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name.Equals(sortBy, StringComparison.OrdinalIgnoreCase));

            if (property == null)
                return query;

            var builder = Builders<T>.Sort;

            var sort = descending
                ? builder.Descending(property.Name)
                : builder.Ascending(property.Name);

            return query.Sort(sort);
        }
    }
}
