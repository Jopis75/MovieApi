using FluentValidation;
using MovieApi.Dtos.Actors;

namespace MovieApi.Validators.Actors
{
    public class CreateActorDtoValidator : AbstractValidator<CreateActorDto>
    {
        public CreateActorDtoValidator()
        {
            RuleFor(createActorDto => createActorDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createActorDto => createActorDto.BirthYear)
                .GreaterThan(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than {DateTime.MinValue.Year}.");
        }
    }
}
