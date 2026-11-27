using FluentValidation;
using MovieApi.Dtos.Genres;

namespace MovieApi.Validators.Genres
{
    public class UpdateGenreDtoValidator : AbstractValidator<UpdateGenreDto>
    {
        public UpdateGenreDtoValidator()
        {
            RuleFor(genreDto => genreDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(genreDto => genreDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
