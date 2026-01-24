using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace PersonService.Infra.Data.Persistence
{
    public static class MongoDbConventions
    {
        private static bool _isRegistered;

        public static void RegisterConventions()
        {
            if (_isRegistered) return;

            var conventionPack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("CustomConventions", conventionPack, _ => true);

            _isRegistered = true;
        }
    }
}
