using Dentizone.Application.Interfaces;
using Dentizone.Infrastructure.ApiClient;

namespace Dentizone.Application.Jobs
{
    public class MonitorJob(
        IAiLayer aiLayer,
        IQaService qaService,
        IReviewService reviewService,
        IPostService postService)
        : IMonitorJob
    {
        public async Task ReviewAnswerAsync(string answerId, string input)
        {
            if (await IsContentToxicAsync(input))
                await qaService.DeleteAnswerAsync(answerId);
        }

        public async Task ReviewQuestionAsync(string questionId, string input)
        {
            if (await IsContentToxicAsync(input))
                await qaService.DeleteQuestionAsync(questionId);
        }

        public async Task ReviewPostAsync(string postId, string input)
        {
            if (await IsContentToxicAsync(input))
                await postService.DeletePost(postId);
        }

        public async Task ReviewReviewAsync(string reviewId, string input)
        {
            if (await IsContentToxicAsync(input))
                await reviewService.DeleteReviewAsync(reviewId);
        }

        private async Task<bool> IsContentToxicAsync(string content)
        {
            var response = await aiLayer.ScanContactToxic(content);

            if (response.Content == null)
                return false;

            var hasContactInfo =
                response.Content.ContactInfo.Emails.Count > 0 ||
                response.Content.ContactInfo.PhoneNumbers.Count > 0 ||
                response.Content.ContactInfo.Addresses.Count > 0;

            return hasContactInfo || response.Content.IsInsult == true;
        }
    }
}