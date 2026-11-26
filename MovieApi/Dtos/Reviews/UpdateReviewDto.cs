namespace MovieApi.Dtos.Reviews
{
    public record class UpdateReviewDto
    {
        public int Id { get; set; }

        public string? ReviewerName { get; set; }

        public string? Comment { get; set; }

        public int Rating { get; set; }
    }
}
