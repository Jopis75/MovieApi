using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class CreateMovieWithMovieDetailsAndGenresDtoValidator : AbstractValidator<CreateMovieWithMovieDetailsAndGenresDto>
    {
        public CreateMovieWithMovieDetailsAndGenresDtoValidator()
        {
            RuleFor(createMovieWithMovieDetailsGenresDtoValidator => createMovieWithMovieDetailsGenresDtoValidator.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            // Den äldsta kända bevarade filmen är Roundhay Garden Scene, som spelades in 1888
            // av den franske uppfinnaren Louis Le Prince.
            RuleFor(createMovieWithMovieDetailsGenresDtoValidator => createMovieWithMovieDetailsGenresDtoValidator.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage("{PropertyName} must be greater than or equal to 1888.");

            RuleFor(createMovieWithMovieDetailsGenresDtoValidator => createMovieWithMovieDetailsGenresDtoValidator.Duration)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createMovieWithMovieDetailsGenresDtoValidator => createMovieWithMovieDetailsGenresDtoValidator.Synopsis)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createMovieWithMovieDetailsGenresDtoValidator => createMovieWithMovieDetailsGenresDtoValidator.Language)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createMovieWithMovieDetailsGenresDtoValidator => createMovieWithMovieDetailsGenresDtoValidator.Budget)
                .GreaterThan(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");

            RuleForEach(createMovieWithMovieDetailsGenresDtoValidator => createMovieWithMovieDetailsGenresDtoValidator.GenreIds)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
