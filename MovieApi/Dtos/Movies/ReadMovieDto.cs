namespace MovieApi.Dtos.Movies
{
    public record ReadMovieDto(int Id, string? Title, int Year, int Duration, int GenreId, int MovieDetailsId);
}
