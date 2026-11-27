namespace MovieApi.Dtos.Genres
{
    public record class CreateGenreWithMovieDto
    {
        public string? Name { get; set; }

        public int MovieId { get; set; }
    }
}
