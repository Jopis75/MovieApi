using MovieApi.Dtos.Movies;

namespace MovieApi.Dtos.Actors
{
    public record class ActorWithMoviesDto : DtoBase
    {
        public string? Name { get; set; }

        public int BirthYear { get; set; }

        public List<MovieDto> Movies { get; set; } = [];
    }
}
