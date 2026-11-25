namespace MovieApi.Dtos.Reviews
{
    public record class ReviewDto : DtoBase
    {
        public string? ReviewerName { get; set; }
        
        public string? Comment { get; set; }
        
        public int Rating { get; set; }
    }
}
