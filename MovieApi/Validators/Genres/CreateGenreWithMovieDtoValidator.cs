using FluentValidation;
using MovieApi.Dtos.Genres;

namespace MovieApi.Validators.Genres
{
    public class CreateGenreWithMovieDtoValidator : AbstractValidator<CreateGenreWithMovieDto>
    {
        public CreateGenreWithMovieDtoValidator()
        {
            RuleFor(createGenreWithMovieDto => createGenreWithMovieDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createGenreWithMovieDto => createGenreWithMovieDto.MovieId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
