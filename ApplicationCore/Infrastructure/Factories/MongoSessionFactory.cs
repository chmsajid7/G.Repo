using ApplicationCore.Common.Interfaces;
using MongoDB.Driver;

namespace ApplicationCore.Infrastructure.Factories;

public sealed class MongoSessionFactory : IMongoSessionFactory
{
    private readonly IMongoClient _client;

    public MongoSessionFactory(IMongoClient client)
        => _client = client;

    public Task<IClientSessionHandle> CreateAsync()
        => _client.StartSessionAsync();
}
