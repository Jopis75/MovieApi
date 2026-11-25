namespace MovieApi.Dtos.Actors
{
    public record class ActorDto : DtoBase
    {
        public string? Name { get; set; }
        
        public int BirthYear { get; set; }
    }
}
