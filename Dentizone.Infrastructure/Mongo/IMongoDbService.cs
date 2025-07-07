using MongoDB.Bson;

namespace Dentizone.Infrastructure.Mongo;

public interface IMongoDbService
{
    Task RegisterAiSystemResponseAsync(string resourceName, string resourceId, string content,
        BsonDocument aiResponse, string functionName);
}