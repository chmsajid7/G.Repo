namespace Infrastructure;

using Domain.Seedwork;
using Infrastructure.Persistence.Configurations;
using MongoDB.Driver;

public class UnitOfWork : IUnitOfWork
{
    public IClientSessionHandle? session { get; set; }
    private IMongoDatabase? database { get; set; }
    public MongoClient? mongoClient { get; set; }
    private readonly List<Func<Task>> commands;

    public UnitOfWork()
    {
        // Every command will be stored and it'll be processed at SaveChangesAsync
        this.commands = new List<Func<Task>>();
    }

    public async Task<bool> SaveChangesAsync()
    {
        MongoDbConfiguration.ConfigureMongoDb();

        ArgumentNullException.ThrowIfNull(this.mongoClient?.StartSessionAsync());

        using (this.session = await this.mongoClient.StartSessionAsync())
        {
            this.session.StartTransaction();

            await Task.WhenAll(this.commands.Select(c => c()));

            await this.session.CommitTransactionAsync();
        }

        return this.commands.Count > 0;
    }

    public void Dispose()
    {
        this.session?.Dispose();
        GC.SuppressFinalize(this);
    }
}
