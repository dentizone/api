namespace Dentizone.Application.DTOs.Analytics
{
    public class PostAnalyticsDto
    {
        public int TotalPosts { get; set; }
        public int PendingPosts { get; set; }

        public decimal AveragePostPrice { get; set; }
        public Dictionary<string, int> PostsByCategory { get; set; } = new();
    }
}