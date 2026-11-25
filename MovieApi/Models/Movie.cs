namespace MovieApi.Models
{
    public class Movie : EntityBase
    {
        public string? Title { get; set; }
        
        public int Year { get; set; }
        
        // Duration in minutes.
        public int Duration { get; set; }

        public int MovieDetailsId { get; set; }

        public virtual MovieDetails? MovieDetails { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = [];

        public virtual ICollection<MovieActor> MovieActors { get; set; } = [];

        public virtual ICollection<MovieGenre> MovieGenres { get; set; } = [];
    }
}
