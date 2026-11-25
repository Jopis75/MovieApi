using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieDetailsConfiguration : IEntityTypeConfiguration<MovieDetails>
    {
        public void Configure(EntityTypeBuilder<MovieDetails> builder)
        {
            builder.HasData(
                new MovieDetails
                {
                    Id = 1,
                    Synopsis = "In 1985, a lonely young boy named John Bennett makes a Christmas wish that his new teddy bear, Ted, will come to life and be his best friend forever. Miraculously, the wish comes true, and Ted becomes a national celebrity and John's inseparable companion.",
                    Language = "English",
                    Budget = 68000000.0M
                }
            );
        }
    }
}
