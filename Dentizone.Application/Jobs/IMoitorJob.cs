namespace Dentizone.Infrastructure.Hangfire
{
    public interface IMoitorJob
    {
        Task ReviewToxicAndPII(string input, string id, string resourceType);
    }
}