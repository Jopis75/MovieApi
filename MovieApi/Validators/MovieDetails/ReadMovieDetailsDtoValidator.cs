using FluentValidation;
using MovieApi.Dtos.MovieDetails;

namespace MovieApi.Validators.MovieDetails
{
    public class ReadMovieDetailsDtoValidator : AbstractValidator<ReadMovieDetailsDto>
    {
        public ReadMovieDetailsDtoValidator()
        {
            RuleFor(readMovieDetailsDto => readMovieDetailsDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(readMovieDetailsDto => readMovieDetailsDto.Synopsis)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(readMovieDetailsDto => readMovieDetailsDto.Language)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(readMovieDetailsDto => readMovieDetailsDto.Budget)
                .GreaterThan(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");
        }
    }
}
