using Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Persistence.Configurations;

public class TodoConfiguration
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<TodoModel>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapIdMember(x => x.Id);
            map.MapMember(x => x.Description).SetIsRequired(true);
        });
    }
}
