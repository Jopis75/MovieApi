using FluentValidation;
using MovieApi.Dtos.Genres;

namespace MovieApi.Validators.Genres
{
    public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
    {
        public CreateGenreDtoValidator()
        {
            RuleFor(createGenreDto => createGenreDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
