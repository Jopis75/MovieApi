namespace MovieApi.Dtos.Movies
{
    public record class UpdateMovieWithMovieDetailsDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public int Year { get; set; }

        public int Duration { get; set; }

        public int MovieDetailsId { get; set; }

        public string? Synopsis { get; set; }

        public string? Language { get; set; }

        public decimal Budget { get; set; }
    }
}
