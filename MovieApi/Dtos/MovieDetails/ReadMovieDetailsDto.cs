namespace MovieApi.Dtos.MovieDetails
{
    public record ReadMovieDetailsDto(int Id, string? Synopsis, string? Language, decimal Budget);
}
