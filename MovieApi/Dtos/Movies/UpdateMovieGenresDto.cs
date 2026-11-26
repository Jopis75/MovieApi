namespace MovieApi.Dtos.Movies
{
    public record class UpdateMovieGenresDto
    {
        public int MovieId { get; set; }

        public List<int> GenreIds { get; set; } = [];
    }
}
