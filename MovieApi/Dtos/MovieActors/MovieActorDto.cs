namespace MovieApi.Dtos.MovieActors
{
    public record class MovieActorDto : DtoBase
    {
        public int MovieId { get; set; }

        public int ActorId { get; set; }
    }
}
