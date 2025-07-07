using Dentizone.Application.Interfaces;
using Dentizone.Infrastructure.ApiClient;
using Dentizone.Infrastructure.Mongo;
using MongoDB.Bson;
using System.Text.Json;

namespace Dentizone.Application.Jobs
{
    public class MonitorJob : IMonitorJob
    {
        private readonly IAiLayer aiLayer;
        private readonly IQaService qaService;
        private readonly IReviewService reviewService;
        private readonly IPostService postService;
        private readonly MongoDbService mongoDbService;

        public MonitorJob(
            IAiLayer aiLayer,
            IQaService qaService,
            IReviewService reviewService,
            IPostService postService,
            MongoDbService mongoDbService)
        {
            this.aiLayer = aiLayer;
            this.qaService = qaService;
            this.reviewService = reviewService;
            this.postService = postService;
            this.mongoDbService = mongoDbService;
        }

        public async Task ReviewAnswerAsync(string answerId, string input)
        {
            var aiResponse = await aiLayer.ScanContactToxic(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Answer", answerId, input, bsonResponse, nameof(aiLayer.ScanContactToxic));
            if (await IsContentToxicAsync(aiResponse))
                await qaService.DeleteAnswerAsync(answerId);
        }

        public async Task ReviewQuestionAsync(string questionId, string input)
        {
            var aiResponse = await aiLayer.ScanContactToxic(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Question", questionId, input, bsonResponse, nameof(aiLayer.ScanContactToxic));
            if (await IsContentToxicAsync(aiResponse))
                await qaService.DeleteQuestionAsync(questionId);
        }

        public async Task ReviewPostAsync(string postId, string input)
        {
            var aiResponse = await aiLayer.ScanContactToxic(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Post", postId, input, bsonResponse, nameof(aiLayer.ScanContactToxic));
            if (await IsContentToxicAsync(aiResponse))
                await postService.DeletePost(postId);
        }

        public async Task ReviewReviewAsync(string reviewId, string input)
        {
            var aiResponse = await aiLayer.GetSetmenetAnalysis(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Review", reviewId, input, bsonResponse, nameof(aiLayer.ScanContactToxic));

        }

        private async Task<bool> IsContentToxicAsync(dynamic aiResponse)
        {
            var content = aiResponse.Content;
            if (content == null)
                return false;
            // Check if it has content.ContactInfo
            var hasContactInfo = false;
            if (content?.ContactInfo is not null)
            {
                hasContactInfo =
            content.ContactInfo.Emails.Count > 0 ||
            content.ContactInfo.PhoneNumbers.Count > 0 ||
            content.ContactInfo.Addresses.Count > 0;
            }


            return hasContactInfo || content.IsInsult == true;
        }

    }
}