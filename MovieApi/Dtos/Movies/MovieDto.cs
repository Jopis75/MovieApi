using MovieApi.Dtos.Actors;
using MovieApi.Dtos.Genres;
using MovieApi.Dtos.MovieDetails;
using MovieApi.Dtos.Reviews;
using MovieApi.Models;

namespace MovieApi.Dtos.Movies
{
    public record class MovieDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public int Year { get; set; }

        public int Duration { get; set; }

        public MovieDetailsDto? MovieDetails { get; set; }

        public List<GenreDto> Genres { get; set; } = [];

        public List<ActorDto> Actors { get; set; } = [];

        public List<ReviewDto> Reviews { get; set; } = [];
    }
}
