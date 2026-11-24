using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

public class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
{
    public DbSet<Actor> Actors { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<MovieActor> MovieActors { get; set; }

    public DbSet<MovieDetails> MovieDetails { get; set; }

    public DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Actor.
        modelBuilder
            .Entity<Actor>()
            .ToTable("Actor");
        modelBuilder
            .Entity<Actor>()
            .HasKey(actor => actor.Id);

        // Genre.
        modelBuilder
            .Entity<Genre>()
            .ToTable("Genre");
        modelBuilder
            .Entity<Genre>()
            .HasKey(genre => genre.Id);
        modelBuilder
            .Entity<Genre>()
            .Property(genre => genre.Name)
            .IsRequired();

        // Movie.
        modelBuilder
            .Entity<Movie>()
            .ToTable("Movie");
        modelBuilder
            .Entity<Movie>()
            .HasKey(movie => movie.Id);
        modelBuilder
            .Entity<Movie>()
            .HasOne(movie => movie.Genre)
            .WithOne(genre => genre.Movie)
            .HasForeignKey<Movie>(movie => movie.GenreId);
        modelBuilder
            .Entity<Movie>()
            .HasOne(movie => movie.MovieDetails)
            .WithOne(movieDetails => movieDetails.Movie)
            .HasForeignKey<Movie>(movie => movie.MovieDetailsId);
        modelBuilder
            .Entity<Movie>()
            .HasMany(movie => movie.Reviews)
            .WithOne(review => review.Movie)
            .HasForeignKey(review => review.MovieId);
        modelBuilder
            .Entity<Movie>()
            .Property(movie => movie.Title)
            .IsRequired();

        // MovieActor.
        modelBuilder
            .Entity<MovieActor>()
            .ToTable("MovieActor");
        modelBuilder
            .Entity<MovieActor>()
            .HasKey(movieActor => movieActor.Id);
        modelBuilder
            .Entity<MovieActor>()
            .HasKey(movieActor => movieActor.Id);
        modelBuilder
            .Entity<MovieActor>()
            .HasOne(movieActor => movieActor.Movie)
            .WithMany(movie => movie.MovieActors)
            .HasForeignKey(movieActor => movieActor.MovieId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        modelBuilder
            .Entity<MovieActor>()
            .HasOne(movieActor => movieActor.Actor)
            .WithMany(actor => actor.MovieActors)
            .HasForeignKey(movieActor => movieActor.ActorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        // MovieDetails.
        modelBuilder
            .Entity<MovieDetails>()
            .ToTable("MovieDetails");
        modelBuilder
            .Entity<MovieDetails>()
            .HasKey(movieDetails => movieDetails.Id);
        modelBuilder
            .Entity<MovieDetails>()
            .HasKey(movieDetails => movieDetails.Id);
        modelBuilder
            .Entity<MovieDetails>()
            .Property(movieDetails => movieDetails.Budget)
            .HasPrecision(18, 2);
        modelBuilder
            .Entity<MovieDetails>()
            .Property(movieDetails => movieDetails.Synopsis)
            .IsRequired();
        modelBuilder
            .Entity<MovieDetails>()
            .Property(movieDetails => movieDetails.Language)
            .IsRequired();

        // Review.
        modelBuilder
            .Entity<Review>()
            .ToTable("Review");
        modelBuilder
            .Entity<Review>()
            .HasKey(review => review.Id);
        modelBuilder
            .Entity<Review>()
            .HasOne(review => review.Movie)
            .WithMany(movie => movie.Reviews)
            .HasForeignKey(review => review.MovieId);
        modelBuilder
            .Entity<Review>()
            .Property(review => review.ReviewerName)
            .IsRequired();
        modelBuilder
            .Entity<Review>()
            .Property(review => review.Comment)
            .IsRequired();
    }
}