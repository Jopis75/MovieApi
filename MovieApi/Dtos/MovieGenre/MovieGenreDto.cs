namespace MovieApi.Dtos.MovieGenre
{
    public record class MovieGenreDto : DtoBase
    {
        public int GenreId { get; set; }

        public int MovieId { get; set; }
    }
}
