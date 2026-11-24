using FluentValidation;
using MovieApi.Dtos.Movies;

namespace MovieApi.Validators.Movies
{
    public class DeleteMovieDtoValidator : AbstractValidator<DeleteMovieDto>
    {
        public DeleteMovieDtoValidator()
        {
            RuleFor(deleteMovieDto => deleteMovieDto.Id)
               .GreaterThan(0)
               .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
