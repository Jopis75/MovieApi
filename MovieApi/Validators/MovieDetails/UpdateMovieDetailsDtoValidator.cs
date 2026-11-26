using FluentValidation;
using MovieApi.Dtos.MovieDetails;

namespace MovieApi.Validators.MovieDetails
{
    public class UpdateMovieDetailsDtoValidator : AbstractValidator<UpdateMovieDetailsDto>
    {
        public UpdateMovieDetailsDtoValidator()
        {
            RuleFor(updateMovieDetailsDto => updateMovieDetailsDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateMovieDetailsDto => updateMovieDetailsDto.Synopsis)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateMovieDetailsDto => updateMovieDetailsDto.Language)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateMovieDetailsDto => updateMovieDetailsDto.Budget)
                .GreaterThan(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");
        }
    }
}
