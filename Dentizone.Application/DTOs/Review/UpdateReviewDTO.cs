namespace Dentizone.Application.DTOs.Review
{
    public class UpdateReviewDto
    {
        public string? Comment { get; set; }
        public int? Stars { get; set; }
        public string? Sentiment { get; set; }
    }
}