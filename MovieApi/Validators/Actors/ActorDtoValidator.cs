using FluentValidation;
using MovieApi.Dtos.Actors;

namespace MovieApi.Validators.Actors
{
    public class ActorDtoValidator : AbstractValidator<ActorDto>
    {
        public ActorDtoValidator()
        {
            RuleFor(actorDto => actorDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(actorDto => actorDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(actorDto => actorDto.BirthYear)
                .GreaterThan(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than {DateTime.MinValue.Year}.");
        }
    }
}
