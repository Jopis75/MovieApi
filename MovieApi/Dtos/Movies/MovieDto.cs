using MovieApi.Dtos.Actors;
using MovieApi.Dtos.Genres;
using MovieApi.Dtos.MovieDetails;
using MovieApi.Dtos.Reviews;

namespace MovieApi.Dtos.Movies
{
    public record class MovieDto : DtoBase
    {
        public string? Title { get; set; }

        public int Year { get; set; }

        public int Duration { get; set; }

        public MovieDetailsDto? MovieDetails { get; set; }

        public List<GenreDto> Genres { get; set; } = [];

        public List<ActorDto> Actors { get; set; } = [];

        public List<ReviewDto> Reviews { get; set; } = [];
    }
}
