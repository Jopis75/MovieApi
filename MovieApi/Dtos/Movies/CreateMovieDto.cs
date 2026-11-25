namespace MovieApi.Dtos.Movies
{
    public record class CreateMovieDto
    {
        public string? Title { get; set; }
        
        public int Year { get; set; }
        
        public int Duration { get; set; }

        public int MovieDetailsId { get; set; }
    }
}
