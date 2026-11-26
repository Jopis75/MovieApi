using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasData(
                new MovieGenre
                {
                    Id = 1,
                    MovieId = 1,
                    GenreId = 3
                },
                new MovieGenre
                {
                    Id = 2,
                    MovieId = 1,
                    GenreId = 7
                },
                new MovieGenre
                {
                    Id = 3,
                    MovieId = 2,
                    GenreId = 3
                },
                new MovieGenre
                {
                    Id = 4,
                    MovieId = 2,
                    GenreId = 7
                }
            );
        }
    }
}
