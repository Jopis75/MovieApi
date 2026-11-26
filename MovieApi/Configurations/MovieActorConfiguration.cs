using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasData(
                new MovieActor
                {
                    Id = 1,
                    MovieId = 1,
                    ActorId = 1
                },
                new MovieActor
                {
                    Id = 2,
                    MovieId = 1,
                    ActorId = 2
                },
                new MovieActor
                {
                    Id = 3,
                    MovieId = 1,
                    ActorId = 3
                },
                new MovieActor
                {
                    Id = 4,
                    MovieId = 1,
                    ActorId = 4
                },
                new MovieActor
                {
                    Id = 5,
                    MovieId = 1,
                    ActorId = 5
                },
                new MovieActor
                {
                    Id = 6,
                    MovieId = 1,
                    ActorId = 6
                },
                new MovieActor
                {
                    Id = 7,
                    MovieId = 1,
                    ActorId = 7
                },
                new MovieActor
                {
                    Id = 8,
                    MovieId = 1,
                    ActorId = 8
                },
                new MovieActor
                {
                    Id = 9,
                    MovieId = 1,
                    ActorId = 9
                },
                new MovieActor
                {
                    Id = 10,
                    MovieId = 1,
                    ActorId = 10
                },
                new MovieActor
                {
                    Id = 11,
                    MovieId = 2,
                    ActorId = 1
                },
                new MovieActor
                {
                    Id = 12,
                    MovieId = 2,
                    ActorId = 2
                },
                new MovieActor
                {
                    Id = 13,
                    MovieId = 2,
                    ActorId = 11
                },
                new MovieActor
                {
                    Id = 14,
                    MovieId = 2,
                    ActorId = 5
                },
                new MovieActor
                {
                    Id = 15,
                    MovieId = 2,
                    ActorId = 8
                },
                new MovieActor
                {
                    Id = 16,
                    MovieId = 2,
                    ActorId = 12
                },
                new MovieActor
                {
                    Id = 17,
                    MovieId = 2,
                    ActorId = 13
                },
                new MovieActor
                {
                    Id = 18,
                    MovieId = 2,
                    ActorId = 6
                },
                new MovieActor
                {
                    Id = 19,
                    MovieId = 2,
                    ActorId = 14
                },
                new MovieActor
                {
                    Id = 20,
                    MovieId = 2,
                    ActorId = 10
                }
            );
        }
    }
}
