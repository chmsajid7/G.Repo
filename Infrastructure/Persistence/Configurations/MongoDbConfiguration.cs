namespace Infrastructure.Persistence.Configurations;

using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

internal class MongoDbConfiguration
{
    private readonly IConfiguration configuration;
    private IMongoDatabase? database { get; set; }
    public MongoClient? mongoClient { get; set; }

    public MongoDbConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration
            ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void ConfigureMongoDb()
    {
        if (this.mongoClient != null)
        {
            return;
        }

        // Configure mongo (You can inject the config, just to simplify)
        this.mongoClient = new MongoClient(this.configuration["MongoSettings:Connection"]);

        this.database = this.mongoClient.GetDatabase(this.configuration["MongoSettings:DatabaseName"]);
    }
}
