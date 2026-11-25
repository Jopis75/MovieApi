namespace MovieApi.Models
{
    public class Genre : EntityBase
    {
        public string? Name { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; } = [];
    }
}
