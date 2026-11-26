namespace MovieApi.Dtos.Reviews
{
    public record class CreateReviewDto
    {
        public string? ReviewerName { get; set; }

        public string? Comment { get; set; }

        // Rating 1-5.
        public int Rating { get; set; }

        public int MovieId { get; set; }
    }
}
