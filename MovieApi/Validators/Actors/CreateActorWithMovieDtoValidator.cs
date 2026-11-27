using FluentValidation;
using MovieApi.Dtos.Actors;

namespace MovieApi.Validators.Actors
{
    public class CreateActorWithMovieDtoValidator : AbstractValidator<CreateActorWithMovieDto>
    {
        public CreateActorWithMovieDtoValidator()
        {
            RuleFor(createActorWithMovieDto => createActorWithMovieDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createActorWithMovieDto => createActorWithMovieDto.BirthYear)
                .GreaterThanOrEqualTo(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than or equal to {DateTime.MinValue.Year}.");

            RuleFor(createActorWithMovieDto => createActorWithMovieDto.MovieId)
                .GreaterThanOrEqualTo(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than or equal to {DateTime.MinValue.Year}.")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName}" + $" must be less than or equal to {DateTime.Now.Year}.");
        }
    }
}
