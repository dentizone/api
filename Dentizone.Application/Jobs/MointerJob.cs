using Dentizone.Application.Interfaces;
using Dentizone.Infrastructure.ApiClient;
using Dentizone.Infrastructure.Mongo;
using MongoDB.Bson;
using System.Text.Json;
using Dentizone.Application.DTOs.Review;

namespace Dentizone.Application.Jobs
{
    public class MonitorJob(
        IAiLayer aiLayer,
        IQaService qaService,
        IReviewService reviewService,
        IPostService postService,
        IMongoDbService mongoDbService)
        : IMonitorJob
    {
        public async Task ReviewAnswerAsync(string answerId, string input)
        {
            var aiResponse = await aiLayer.ScanContactToxic(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Answer", answerId, input, bsonResponse,
                nameof(aiLayer.ScanContactToxic));
            if (IsContentToxicAsync(aiResponse))
                await qaService.DeleteAnswerAsync(answerId);
        }

        public async Task ReviewQuestionAsync(string questionId, string input)
        {
            var aiResponse = await aiLayer.ScanContactToxic(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Question", questionId, input, bsonResponse,
                nameof(aiLayer.ScanContactToxic));
            if (IsContentToxicAsync(aiResponse))
                await qaService.DeleteQuestionAsync(questionId);
        }

        public async Task ReviewPostAsync(string postId, string input)
        {
            var aiResponse = await aiLayer.ScanContactToxic(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Post", postId, input, bsonResponse,
                nameof(aiLayer.ScanContactToxic));
            if (IsContentToxicAsync(aiResponse))
                await postService.DeletePost(postId);
        }

        public async Task ReviewReviewAsync(string reviewId, string input)
        {
            var aiResponse = await aiLayer.GetSetmenetAnalysis(input);
            var bsonResponse = BsonDocument.Parse(JsonSerializer.Serialize(aiResponse.Content));
            await mongoDbService.RegisterAiSystemResponseAsync("Review", reviewId, input, bsonResponse,
                nameof(aiLayer.ScanContactToxic));

            if (aiResponse.Content == null)
                throw new InvalidOperationException("Sentiment value is null in AI response.");

            await reviewService.UpdateReviewAsync(reviewId,
                new UpdateReviewDto() { Sentiment = aiResponse.Content.SentimentValue });
        }

        private static bool IsContentToxicAsync(dynamic aiResponse)
        {
            var content = aiResponse.Content;
            if (content == null)
                return false;
            var hasContactInfo = false;
            if (content.ContactInfo is not null)
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