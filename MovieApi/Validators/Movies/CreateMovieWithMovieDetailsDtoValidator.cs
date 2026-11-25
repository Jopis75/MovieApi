using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class CreateMovieWithMovieDetailsDtoValidator : AbstractValidator<CreateMovieWithMovieDetailsDto>
    {
        public CreateMovieWithMovieDetailsDtoValidator()
        {
            RuleFor(createMovieWithMovieDetailsDtoValidator => createMovieWithMovieDetailsDtoValidator.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            // Den äldsta kända bevarade filmen är Roundhay Garden Scene, som spelades in 1888
            // av den franske uppfinnaren Louis Le Prince.
            RuleFor(createMovieWithMovieDetailsDtoValidator => createMovieWithMovieDetailsDtoValidator.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage("{PropertyName} must be greater than or equal to 1888.");

            RuleFor(createMovieWithMovieDetailsDtoValidator => createMovieWithMovieDetailsDtoValidator.Duration)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createMovieWithMovieDetailsDtoValidator => createMovieWithMovieDetailsDtoValidator.Synopsis)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createMovieWithMovieDetailsDtoValidator => createMovieWithMovieDetailsDtoValidator.Language)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createMovieWithMovieDetailsDtoValidator => createMovieWithMovieDetailsDtoValidator.Budget)
                .GreaterThan(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");
        }
    }
}
