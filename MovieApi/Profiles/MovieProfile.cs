using AutoMapper;
using MovieApi.Dtos.Actors;
using MovieApi.Dtos.Genres;
using MovieApi.Dtos.MovieActors;
using MovieApi.Dtos.MovieDetails;
using MovieApi.Dtos.MovieGenres;
using MovieApi.Dtos.Movies;
using MovieApi.Dtos.Reviews;
using MovieApi.Models;

namespace MovieApi.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            // Actor.
            CreateMap<Actor, CreateActorDto>().ReverseMap();
            CreateMap<Actor, CreateActorWithMovieDto>().ReverseMap();
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<Actor, ActorWithMoviesDto>().ReverseMap();
            CreateMap<Actor, ActorWithMoviesDto>()
                .ForMember(actorWithMoviesDto => actorWithMoviesDto.Movies,
                    cfg => cfg.MapFrom(actor => actor.MovieActors.Select(movieActor => movieActor.Movie).ToList()));
            CreateMap<Actor, UpdateActorDto>().ReverseMap();

            // Genre.
            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, CreateGenreWithMovieDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Genre, UpdateGenreDto>().ReverseMap();

            // Movie.
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, CreateMovieWithMovieDetailsAndGenresDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, MovieDto>()
                .ForMember(movieDto => movieDto.Actors,
                    cfg => cfg.MapFrom(movie => movie.MovieActors.Select(movieActor => movieActor.Actor).ToList()))
                .ForMember(movieDto => movieDto.Genres,
                    cfg => cfg.MapFrom(movie => movie.MovieGenres.Select(movieGenre => movieGenre.Genre).ToList()));
            CreateMap<Movie, UpdateMovieWithMovieDetailsDto>().ReverseMap();

            // MovieActor.
            CreateMap<MovieActor, CreateActorWithMovieDto>().ReverseMap();
            CreateMap<MovieActor, MovieActorDto>().ReverseMap();

            // MovieDetails.
            CreateMap<MovieDetails, CreateMovieWithMovieDetailsAndGenresDto>().ReverseMap();
            CreateMap<MovieDetails, CreateMovieWithMovieDetailsDto>().ReverseMap();
            CreateMap<MovieDetails, MovieDetailsDto>().ReverseMap();
            CreateMap<MovieDetails, UpdateMovieDetailsDto>().ReverseMap();
            CreateMap<MovieDetails, UpdateMovieWithMovieDetailsDto>().ReverseMap();
            CreateMap<UpdateMovieWithMovieDetailsDto, MovieDetails>()
                .ForMember(movieDetails => movieDetails.Id,
                    cfg => cfg.MapFrom(updateMovieDto => updateMovieDto.MovieDetailsId));

            // MovieGenre.
            CreateMap<MovieGenre, MovieGenreDto>().ReverseMap();

            // Review.
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, ReviewWithMovieDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
        }
    }
}
