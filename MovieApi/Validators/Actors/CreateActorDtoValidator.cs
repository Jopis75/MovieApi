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
                .GreaterThanOrEqualTo(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than or equal to {DateTime.MinValue.Year}.")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName}" + $" must be less than or equal to {DateTime.Now.Year}.");
        }
    }
}
