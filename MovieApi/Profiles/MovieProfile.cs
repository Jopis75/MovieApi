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
            CreateMap<Actor, ReadActorDto>().ReverseMap();

            // Genre.
            CreateMap<Genre, ReadGenreDto>().ReverseMap();

            // Movie.
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, ReadMovieDto>().ReverseMap();
            CreateMap<Movie, UpdateMovieDto>().ReverseMap();
            CreateMap<Movie, DeleteMovieDto>().ReverseMap();

            // MovieDetails.
            CreateMap<MovieDetails, ReadMovieDetailsDto>().ReverseMap();

            // Review.
            CreateMap<Review, ReadReviewDto>().ReverseMap();
        }
    }
}
