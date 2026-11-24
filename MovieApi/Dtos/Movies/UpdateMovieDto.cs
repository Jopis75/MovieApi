namespace MovieApi.Dtos.Movies
{
    public record UpdateMovieDto(int Id, string? Title, int Year, int Duration, int GenreId, int MovieDetailsId);
}
