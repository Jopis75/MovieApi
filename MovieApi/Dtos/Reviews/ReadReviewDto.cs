namespace MovieApi.Dtos.Reviews
{
    public record ReadReviewDto(int Id, string? ReviewerName, string? Comment, int Rating, int MovieId);
}
