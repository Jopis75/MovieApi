using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            //• Seth MacFarlane – rösten till Ted / regissör
            //• Mark Wahlberg – John Bennett
            //• Amanda Seyfried – Samantha Jackson(advokaten)
            //• Jessica Barth – Tami - Lynn
            //• Giovanni Ribisi – Donny
            //• Morgan Freeman – Patrick Meighan
            //• John Slattery – Shep Wild
            //• Patrick Warburton – Guy
            //• Michael Dorn – Rick
            //• Patrick Stewart – Berättare(röst)

            builder.HasData(
                new Actor
                {
                    Id = 1,
                    Name = "Seth MacFarlane",
                    BirthYear = 1973
                },
                new Actor
                {
                    Id = 2,
                    Name = "Mark Wahlberg",
                    BirthYear = 1971
                },
                new Actor
                {
                    Id = 3,
                    Name = "Mila Kunis",
                    BirthYear = 1983
                },
                new Actor
                {
                    Id = 4,
                    Name = "Joel McHale",
                    BirthYear = 1971
                },
                new Actor
                {
                    Id = 5,
                    Name = "Giovanni Ribisi",
                    BirthYear = 1974
                },
                new Actor
                {
                    Id = 6,
                    Name = "Patrick Warburton",
                    BirthYear = 1964
                },
                new Actor
                {
                    Id = 7,
                    Name = "Matt Walsh",
                    BirthYear = 1964
                },
                new Actor
                {
                    Id = 8,
                    Name = "Jessica Barth",
                    BirthYear = 1980
                },
                new Actor
                {
                    Id = 9,
                    Name = "Bill Smitrovich",
                    BirthYear = 1947
                },
                new Actor
                {
                    Id = 10,
                    Name = "Patrick Stewart",
                    BirthYear = 1940
                },
                new Actor
                {
                    Id = 11,
                    Name = "Amanda Seyfried",
                    BirthYear = 1985
                },
                new Actor
                {
                    Id = 12,
                    Name = "Morgan Freeman",
                    BirthYear = 1937
                },
                new Actor
                {
                    Id = 13,
                    Name = "John Slattery",
                    BirthYear = 1962
                },
                new Actor
                {
                    Id = 14,
                    Name = "Michael Dorn",
                    BirthYear = 1952
                }
            );
        }
    }
}
