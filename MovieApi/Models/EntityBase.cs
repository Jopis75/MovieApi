using MovieApi.Models.Interfaces;

namespace MovieApi.Models
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
    }
}
