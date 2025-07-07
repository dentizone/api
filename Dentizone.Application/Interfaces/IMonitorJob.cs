namespace Dentizone.Application.Interfaces
{
    public interface IMonitorJob
    {
        Task ReviewQuestionAsync(string questionId, string input);
        Task ReviewPostAsync(string postId, string input);
        Task ReviewAnswerAsync(string answerId, string input);
        Task ReviewReviewAsync(string reviewId, string input);
    }
}