using ApplicationCore.Common.Interfaces;
using ApplicationCore.Infrastructure.Factories;
using ApplicationCore.Models;
using Infrastructure.Builders;
using Infrastructure.Persistence;
using MongoDB.Driver;

namespace API.Extensions;

public static class CustomExtensionsMethods
{
    public static WebApplicationBuilder AddMongo(this WebApplicationBuilder builder)
    {
        var mongoOptions = buildOptions(new MongoDbOptionsBuilder()).Build();

        builder.Services.AddSingleton(mongoOptions);

        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetService<MongoDbOptions>();
            return new MongoClient(options?.ConnectionString);
        });

        builder.Services.AddTransient(sp =>
        {
            var options = sp.GetService<MongoDbOptions>();
            var client = sp.GetService<IMongoClient>();
            return client?.GetDatabase(options?.Database);
        });

        builder.Services.AddTransient<MongoDbPersistence>();
        builder.Services.AddTransient<IMongoSessionFactory, MongoSessionFactory>();

        return builder;
    }
}
