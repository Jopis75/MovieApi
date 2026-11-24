namespace MovieApi.Models
{
    public class MovieDetails : EntityBase
    {
        public string? Synopsis { get; set; }
        
        public string? Language { get; set; }
        
        public decimal Budget { get; set; }

        public virtual Movie? Movie { get; set; }
    }
}
