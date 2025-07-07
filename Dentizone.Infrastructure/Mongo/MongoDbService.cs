using MongoDB.Driver;
using MongoDB.Bson;
using Dentizone.Domain.Interfaces.Secret;

namespace Dentizone.Infrastructure.Mongo
{
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

    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        public IMongoDatabase Database => _database;

        public MongoDbService(ISecretService secretService)
        {
            var connectionString = secretService.GetSecret("MongoDbConnectionString");
            var databaseName = secretService.GetSecret("MongoDbDatabaseName");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public async Task RegisterAiSystemResponseAsync(string resourceName, string resourceId, string content, BsonDocument aiResponse, string functionName)
        {
            var collection = _database.GetCollection<AiSystemResponse>("ai_system_responses");
            var response = new AiSystemResponse
            {
                ResourceName = resourceName,
                ResourceId = resourceId,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                AiResponse = aiResponse,
                FunctionName = functionName
            };
            await collection.InsertOneAsync(response);
        }
    }
}
