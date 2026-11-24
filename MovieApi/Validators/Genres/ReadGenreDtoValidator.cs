using FluentValidation;
using MovieApi.Dtos.Genres;

namespace MovieApi.Validators.Genres
{
    public class ReadGenreDtoValidator : AbstractValidator<ReadGenreDto>
    {
        public ReadGenreDtoValidator()
        {
            RuleFor(readGenreDto => readGenreDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(readGenreDto => readGenreDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
