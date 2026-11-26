namespace MovieApi.Dtos.MovieGenres
{
    public record class MovieGenreDto : DtoBase
    {
        public int GenreId { get; set; }

        public int MovieId { get; set; }
    }
}
