namespace Dentizone.Infrastructure.Hangfire
{
    internal interface IMoitorJob
    {
        Task ReviewToxicAndPII(string input, string id, string resourceType);
    }
}