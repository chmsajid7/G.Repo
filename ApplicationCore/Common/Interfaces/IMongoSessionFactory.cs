using MongoDB.Driver;

namespace ApplicationCore.Common.Interfaces;

public interface IMongoSessionFactory
{
    Task<IClientSessionHandle> CreateAsync();
}
