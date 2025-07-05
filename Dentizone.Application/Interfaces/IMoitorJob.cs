namespace Dentizone.Application.Interfaces
{
    public interface IMoitorJob
    {
        Task ReviewToxicAndPII(string input, string id, string resourceType);
    }
}