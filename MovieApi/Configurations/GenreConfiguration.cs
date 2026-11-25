using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Action"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Adventure"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Comedy"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Drama"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Horror"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Id = 7,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 8,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 9,
                    Name = "Thriller & Mystery"
                },
                new Genre
                {
                    Id = 10,
                    Name = "Documentary"
                }
            );
        }
    }
}
