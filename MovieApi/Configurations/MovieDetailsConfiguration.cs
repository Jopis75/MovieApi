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
                    Synopsis = "A man whose childhood wish of bringing his teddy bear to life came true must now decide whether that friendship is more important to him than his girlfriend.",
                    Language = "English",
                    Budget = 60000000.0M
                },
                new MovieDetails
                {
                    Id = 2,
                    Synopsis = "Newlywed couple Ted and Tami-Lynn want to have a baby, but in order to qualify to be a parent, Ted will have to prove he's a person in a court of law.",
                    Language = "English",
                    Budget = 68000000.0M
                }
            );
        }
    }
}
