namespace MovieApi.Models
{
    public class MovieActor : EntityBase
    {
        public int MovieId { get; set; }

        public virtual Movie? Movie { get; set; }

        public int ActorId { get; set; }

        public virtual Actor? Actor { get; set; }
    }
}
