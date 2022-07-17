using ApplicationCore.Common.Interfaces;
using ApplicationCore.Models;

namespace Infrastructure.Builders;

public sealed class MongoDbOptionsBuilder : IMongoDbOptionsBuilder
{
    private readonly MongoDbOptions options = new MongoDbOptions();

    public IMongoDbOptionsBuilder WithConnectionString(string connectionString)
    {
        this.options.ConnectionString = connectionString;
        return this;
    }

    public IMongoDbOptionsBuilder WithDatabase(string database)
    {
        this.options.Database = database;
        return this;
    }

    public MongoDbOptions Build()
        => this.options;
}
