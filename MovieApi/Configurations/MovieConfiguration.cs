using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Ted",
                    Year = 2012,
                    Duration = 106,
                    MovieDetailsId = 1
                },
                new Movie
                {
                    Id = 2,
                    Title = "Ted 2",
                    Year = 2015,
                    Duration = 116,
                    MovieDetailsId = 2
                }
            );
        }
    }
}
