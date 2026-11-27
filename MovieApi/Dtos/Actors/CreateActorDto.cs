namespace MovieApi.Dtos.Actors
{
    public record class CreateActorDto
    {
        public string? Name { get; set; }

        public int BirthYear { get; set; }
    }
}
