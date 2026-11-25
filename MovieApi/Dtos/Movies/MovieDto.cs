using MovieApi.Dtos.Genres;
using MovieApi.Dtos.MovieDetails;
using MovieApi.Models;

namespace MovieApi.Dtos.Movies
{
    public record MovieDto(int Id, string? Title, int Year, int Duration, GenreDto Genre, MovieDetailsDto MovieDetails, List<Review> Reviews);
}
