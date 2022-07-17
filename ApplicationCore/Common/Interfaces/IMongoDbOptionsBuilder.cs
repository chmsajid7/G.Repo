using ApplicationCore.Models;

namespace ApplicationCore.Common.Interfaces;

public interface IMongoDbOptionsBuilder
{
    IMongoDbOptionsBuilder WithConnectionString(string connectionString);
    IMongoDbOptionsBuilder WithDatabase(string database);
    MongoDbOptions Build();
}
