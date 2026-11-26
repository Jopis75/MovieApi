namespace MovieApi.Dtos.MovieDetails
{
    public record class UpdateMovieDetailsDto
    {
        public int Id { get; set; }

        public string? Synopsis { get; set; }

        public string? Language { get; set; }

        public decimal Budget { get; set; }
    }
}
