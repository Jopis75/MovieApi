namespace MovieApi.Dtos.Movies
{
    public record class CreateMovieWithMovieDetailsAndGenresDto
    {
        public string? Title { get; set; }

        public int Year { get; set; }

        public int Duration { get; set; }

        public string? Synopsis { get; set; }

        public string? Language { get; set; }

        public decimal Budget { get; set; }

        public List<int> GenreIds { get; set; } = [];
    }
}
