namespace MovieApi.Dtos.Actors
{
    public record class UpdateActorDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int BirthYear { get; set; }
    }
}
