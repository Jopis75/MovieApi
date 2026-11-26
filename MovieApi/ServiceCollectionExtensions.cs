using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos.Actors;
using MovieApi.Dtos.Genres;
using MovieApi.Dtos.MovieDetails;
using MovieApi.Dtos.Movies;
using MovieApi.Dtos.Reviews;
using MovieApi.Profiles;
using MovieApi.Validators.Actors;
using MovieApi.Validators.Genres;
using MovieApi.Validators.MovieDetails;
using MovieApi.Validators.Movies;
using MovieApi.Validators.Reviews;

namespace MovieApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext(configuration);
            serviceCollection.AddValidators();
            serviceCollection.AddAutoMapper();
            return serviceCollection;
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
        {
            // services.AddAutoMapper(cfg => { }, typeof(ProfileTypeFromAssembly1), typeof(ProfileTypeFromAssembly2) /*, ...*/);
            serviceCollection.AddAutoMapper(cfg => { }, typeof(MovieProfile));
            return serviceCollection;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MovieConnectionString") ?? throw new InvalidOperationException("Connection string 'MovieDbContext' not found.");
            serviceCollection.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));
            return serviceCollection;
        }

        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            // Actor.
            serviceCollection.AddScoped<IValidator<CreateActorDto>, CreateActorDtoValidator>();
            serviceCollection.AddScoped<IValidator<CreateActorWithMovieDto>, CreateActorWithMovieDtoValidator>();
            serviceCollection.AddScoped<IValidator<ActorDto>, ActorDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateActorDto>, UpdateActorDtoValidator>();

            // Genre.
            serviceCollection.AddScoped<IValidator<GenreDto>, GenreDtoValidator>();

            // Movie.
            serviceCollection.AddScoped<IValidator<CreateMovieWithMovieDetailsAndGenresDto>, CreateMovieWithMovieDetailsAndGenresDtoValidator>();
            serviceCollection.AddScoped<IValidator<CreateMovieWithMovieDetailsDto>, CreateMovieWithMovieDetailsDtoValidator>();
            serviceCollection.AddScoped<IValidator<MovieDto>, MovieDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateMovieGenresDto>, UpdateMovieGenresDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateMovieWithMovieDetailsDto>, UpdateMovieWithMovieDetailsDtoValidator>();
            
            // MovieDetails.
            serviceCollection.AddScoped<IValidator<MovieDetailsDto>, MovieDetailsDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateMovieDetailsDto>, UpdateMovieDetailsDtoValidator>();

            // Review.
            serviceCollection.AddScoped<IValidator<CreateReviewDto>, CreateReviewDtoValidator>();
            serviceCollection.AddScoped<IValidator<ReviewDto>, ReviewDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateReviewDto>, UpdateReviewDtoValidator>();

            return serviceCollection;
        }
    }
}
