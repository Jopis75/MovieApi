using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
    {
        public CreateMovieDtoValidator()
        {
            RuleFor(createMovieDto => createMovieDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            // Den äldsta kända bevarade filmen är Roundhay Garden Scene, som spelades in 1888
            // av den franske uppfinnaren Louis Le Prince.
            RuleFor(createMovieDto => createMovieDto.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage("{PropertyName} must be greater than or equal to 1888.");

            RuleFor(createMovieDto => createMovieDto.Duration)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createMovieDto => createMovieDto.MovieDetailsId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createMovieDto => createMovieDto.GenreId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
