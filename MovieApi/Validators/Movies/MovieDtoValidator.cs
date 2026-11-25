using FluentValidation;
using MovieApi.Dtos.Movies;
using MovieApi.Validators.Genres;
using MovieApi.Validators.MovieDetails;

namespace MovieApi.Validators.Movies
{
    public class MovieDtoValidator : AbstractValidator<MovieDto>
    {
        public MovieDtoValidator()
        {
            RuleFor(movieDto => movieDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(movieDto => movieDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            // Den äldsta kända bevarade filmen är Roundhay Garden Scene, som spelades in 1888
            // av den franske uppfinnaren Louis Le Prince.
            RuleFor(movieDto => movieDto.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage("{PropertyName} must be greater than or equal to 1888.");

            RuleFor(movieDto => movieDto.Duration)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            //RuleFor(movieDto => movieDto.MovieDetails)
            //    .NotNull()
            //    .SetValidator(new MovieDetailsDtoValidator());

            //RuleFor(movieDto => movieDto.Genre)
            //    .NotNull()
            //    .SetValidator(new GenreDtoValidator());

            //RuleForEach
        }
    }
}
