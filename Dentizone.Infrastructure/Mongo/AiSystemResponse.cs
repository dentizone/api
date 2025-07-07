using MongoDB.Bson;

namespace Dentizone.Infrastructure.Mongo;

public class AiSystemResponse
{
    public ObjectId Id { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string ResourceId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string FunctionName { get; set; } = string.Empty;
    public BsonDocument AiResponse { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}