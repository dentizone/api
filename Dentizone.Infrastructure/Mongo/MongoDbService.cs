using MongoDB.Driver;
using MongoDB.Bson;
using Dentizone.Domain.Interfaces.Secret;
using Microsoft.Extensions.Logging;

namespace Dentizone.Infrastructure.Mongo
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(ISecretService secretService, ILogger<MongoDbService> logger)
        {
            try
            {
                var connectionString = secretService.GetSecret("MongoDbConnectionString");
                var databaseName = secretService.GetSecret("MongoDbDatabaseName");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentException("MongoDB connection string is not configured.");
                }

                if (string.IsNullOrEmpty(databaseName))
                {
                    throw new ArgumentException("MongoDB database name is not configured.");
                }

                var client = new MongoClient(connectionString);
                _database = client.GetDatabase(databaseName);

                logger.LogInformation("MongoDbService initialized successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to initialize MongoDbService.");
                throw;
            }
        }

        public async Task RegisterAiSystemResponseAsync(string resourceName, string resourceId, string content,
            BsonDocument aiResponse, string functionName)
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