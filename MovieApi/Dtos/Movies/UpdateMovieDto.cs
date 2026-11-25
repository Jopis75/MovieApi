namespace MovieApi.Dtos.Movies
{
    public record class UpdateMovieDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public int Year { get; set; }

        public int Duration { get; set; }
    }
}
