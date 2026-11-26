using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class UpdateMovieGenresDtoValidator : AbstractValidator<UpdateMovieGenresDto>
    {
        public UpdateMovieGenresDtoValidator()
        {
            RuleFor(updateMovieGenresDto => updateMovieGenresDto.MovieId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleForEach(updateMovieGenresDto => updateMovieGenresDto.GenreIds)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
