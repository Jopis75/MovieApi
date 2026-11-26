using FluentValidation;
using MovieApi.Dtos.Actors;

namespace MovieApi.Validators.Actors
{
    public class UpdateActorDtoValidator : AbstractValidator<UpdateActorDto>
    {
        public UpdateActorDtoValidator()
        {
            RuleFor(updateActorDto => updateActorDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateActorDto => updateActorDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateActorDto => updateActorDto.BirthYear)
                .GreaterThan(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than {DateTime.MinValue.Year}.");
        }
    }
}
