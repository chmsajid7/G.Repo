using MongoDB.Driver;

namespace Domain.Seedwork;

public interface IMongoDbContext<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity obj);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    void Update(TEntity obj);
    void Remove(Guid id);
    IMongoCollection<T> GetCollection<T>(string name);
}
