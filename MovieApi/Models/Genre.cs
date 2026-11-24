namespace MovieApi.Models
{
    public class Genre : EntityBase
    {
        public string? Name { get; set; }

        public virtual Movie? Movie { get; set; }
    }
}
