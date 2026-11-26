using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(
                new Review
                {
                    Id = 1,
                    ReviewerName = "Jopis",
                    Rating = 5,
                    Comment = "En kanon film! Mycket skratt. Måste bara ses.",
                    MovieId = 1
                },
                new Review
                {
                    Id = 2,
                    ReviewerName = "Jopis",
                    Rating = 5,
                    Comment = "En fantastiskt bra uppföljare till Ted filmen. Lika rolig och spännande.",
                    MovieId = 2
                }
            );
        }
    }
}
