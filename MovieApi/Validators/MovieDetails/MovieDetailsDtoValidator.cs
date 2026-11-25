using FluentValidation;
using MovieApi.Dtos.MovieDetails;

namespace MovieApi.Validators.MovieDetails
{
    public class MovieDetailsDtoValidator : AbstractValidator<MovieDetailsDto>
    {
        public MovieDetailsDtoValidator()
        {
            RuleFor(movieDetailsDto => movieDetailsDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(movieDetailsDto => movieDetailsDto.Synopsis)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(movieDetailsDto => movieDetailsDto.Language)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(movieDetailsDto => movieDetailsDto.Budget)
                .GreaterThan(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");
        }
    }
}
