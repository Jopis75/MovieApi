namespace MovieApi.Dtos.MovieDetails
{
    public record class MovieDetailsDto : DtoBase
    {
        public string? Synopsis { get; set; }
        
        public string? Language { get; set; }
        
        public decimal Budget { get; set; }
    }
}
