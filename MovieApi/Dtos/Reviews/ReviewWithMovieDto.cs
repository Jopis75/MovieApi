using MovieApi.Dtos.Movies;

namespace MovieApi.Dtos.Reviews
{
    public record class ReviewWithMovieDto : DtoBase
    {
        public string? ReviewerName { get; set; }

        public string? Comment { get; set; }

        public int Rating { get; set; }

        public MovieDto? Movie { get; set; }
    }
}
