namespace MovieApi.Models
{
    public class Actor : EntityBase
    {
        public string? Name { get; set; }
        
        public int BirthYear { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; } = [];
    }
}
