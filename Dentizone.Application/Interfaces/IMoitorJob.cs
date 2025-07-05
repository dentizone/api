namespace Dentizone.Application.Interfaces
{
    public interface IMoitorJob
    {
       
        Task ReviewQuestionAsync(string QId, string input);
        Task ReviewPostAsync(string PId, string input);
        Task ReviewAnswerAsync(string AId, string input);
        Task ReviewTheReviewtAsync(string RId, string input);
    }
}