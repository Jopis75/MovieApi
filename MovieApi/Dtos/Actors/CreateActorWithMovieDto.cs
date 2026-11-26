namespace MovieApi.Dtos.Actors
{
    public record class CreateActorWithMovieDto
    {
        public string? Name { get; set; }

        public int BirthYear { get; set; }

        public int MovieId { get; set; }
    }
}
