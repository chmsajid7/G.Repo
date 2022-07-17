using Infrastructure.Persistence.Configurations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence;

public class MongoDbPersistence
{
    private static int initialized;

    public Task InitializeAsync()
    {
        if (Interlocked.Exchange(ref initialized, 1) == 1)
        {
            return Task.CompletedTask;
        }

        Configure();

        RegisterConventions();

        return Task.CompletedTask;
    }

    private void Configure()
    {
        TodoConfiguration.Configure();
    }

    private void RegisterConventions()
    {
        BsonSerializer.RegisterSerializer(typeof(decimal),
            new DecimalSerializer(BsonType.Decimal128));

        BsonSerializer.RegisterSerializer(typeof(decimal?),
            new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

        ConventionRegistry.Register("convey_conventions", new MongoDbConventions(), x => true);
    }

    private class MongoDbConventions : IConventionPack
    {
        public IEnumerable<IConvention> Conventions => new List<IConvention>
        {
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
            new CamelCaseElementNameConvention()
        };
    }
}
