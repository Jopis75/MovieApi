using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class ReadMovieDtoValidator : AbstractValidator<ReadMovieDto>
    {
        public ReadMovieDtoValidator()
        {
            RuleFor(readMovieDto => readMovieDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(readMovieDto => readMovieDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            // Den äldsta kända bevarade filmen är Roundhay Garden Scene, som spelades in 1888
            // av den franske uppfinnaren Louis Le Prince.
            RuleFor(readMovieDto => readMovieDto.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage("{PropertyName} must be greater than or equal to 1888.");

            RuleFor(readMovieDto => readMovieDto.Duration)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(readMovieDto => readMovieDto.MovieDetailsId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(readMovieDto => readMovieDto.GenreId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
