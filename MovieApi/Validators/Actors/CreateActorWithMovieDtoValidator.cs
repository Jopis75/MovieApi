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
                .GreaterThan(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than {DateTime.MinValue.Year}.");

            RuleFor(createActorWithMovieDto => createActorWithMovieDto.MovieId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
