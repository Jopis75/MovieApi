using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class CreateMovieWithMovieDetailsDtoValidator : AbstractValidator<CreateMovieWithMovieDetailsDto>
    {
        public CreateMovieWithMovieDetailsDtoValidator()
        {
            RuleFor(createMovieWithMovieDetailsDto => createMovieWithMovieDetailsDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            // Den äldsta kända bevarade filmen är Roundhay Garden Scene, som spelades in 1888
            // av den franske uppfinnaren Louis Le Prince.
            RuleFor(createMovieWithMovieDetailsDto => createMovieWithMovieDetailsDto.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage("{PropertyName} must be greater than or equal to 1888.");

            RuleFor(createMovieWithMovieDetailsDto => createMovieWithMovieDetailsDto.Duration)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createMovieWithMovieDetailsDto => createMovieWithMovieDetailsDto.Synopsis)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createMovieWithMovieDetailsDto => createMovieWithMovieDetailsDto.Language)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createMovieWithMovieDetailsDto => createMovieWithMovieDetailsDto.Budget)
                .GreaterThan(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");
        }
    }
}
