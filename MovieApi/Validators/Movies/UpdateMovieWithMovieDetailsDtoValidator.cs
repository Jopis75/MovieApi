using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class UpdateMovieWithMovieDetailsDtoValidator : AbstractValidator<UpdateMovieWithMovieDetailsDto>
    {
        public UpdateMovieWithMovieDetailsDtoValidator()
        {
            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            // Den äldsta kända bevarade filmen är Roundhay Garden Scene, som spelades in 1888
            // av den franske uppfinnaren Louis Le Prince.
            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage("{PropertyName} must be greater than or equal to 1888.");

            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.Duration)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.MovieDetailsId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.Synopsis)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.Language)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateMovieWithMoviesDetailsDto => updateMovieWithMoviesDetailsDto.Budget)
                .GreaterThan(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");
        }
    }
}
