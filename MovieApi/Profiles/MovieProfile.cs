using AutoMapper;
using MovieApi.Dtos.Actors;
using MovieApi.Dtos.Genres;
using MovieApi.Dtos.MovieDetails;
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
            CreateMap<Actor, ActorDto>().ReverseMap();

            // Genre.
            CreateMap<Genre, GenreDto>().ReverseMap();

            // Movie.
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, CreateMovieWithMovieDetailsDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, MovieDto>()
              .ForMember(movieDto => movieDto.Actors,
                cfg => cfg.MapFrom(movie => movie.MovieActors.Select(movieActor => movieActor.Actor).ToList()))
              .ForMember(movieDto => movieDto.Genres,
                cfg => cfg.MapFrom(movie => movie.MovieGenres.Select(movieGenre => movieGenre.Genre).ToList()));
            CreateMap<Movie, UpdateMovieDto>().ReverseMap();

            // MovieDetails.
            CreateMap<MovieDetails, CreateMovieWithMovieDetailsDto>().ReverseMap();
            CreateMap<MovieDetails, MovieDetailsDto>().ReverseMap();

            // Review.
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
