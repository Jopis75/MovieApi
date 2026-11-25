namespace MovieApi.Dtos.Reviews
{
    public record ReviewDto(int Id, string? ReviewerName, string? Comment, int Rating, int MovieId);
}
