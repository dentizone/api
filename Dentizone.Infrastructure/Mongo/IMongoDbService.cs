using MongoDB.Bson;
using System.Collections.Generic;
using Dentizone.Domain.Entity;

namespace Dentizone.Infrastructure.Mongo;

public interface IMongoDbService
{
    Task RegisterAiSystemResponseAsync(string resourceName, string resourceId, string content,
        BsonDocument aiResponse, string functionName);

    Task<List<AiSystemResponse>> FetchReviewsAsync();
}