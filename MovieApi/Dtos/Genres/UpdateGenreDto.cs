namespace MovieApi.Dtos.Genres
{
    public record class UpdateGenreDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
