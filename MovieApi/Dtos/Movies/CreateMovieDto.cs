namespace MovieApi.Dtos.Movies
{
    public record CreateMovieDto(string? Title, int Year, int Duration, int MovieDetailsId, int GenreId);
}
