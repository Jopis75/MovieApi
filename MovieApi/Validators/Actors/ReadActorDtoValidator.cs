using FluentValidation;
using MovieApi.Dtos.Actors;

namespace MovieApi.Validators.Actors
{
    public class ReadActorDtoValidator : AbstractValidator<ReadActorDto>
    {
        public ReadActorDtoValidator()
        {
            RuleFor(readActorDto => readActorDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(readActorDto => readActorDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(readActorDto => readActorDto.BirthYear)
                .GreaterThan(DateTime.MinValue.Year)
                .WithMessage("{PropertyName}" + $" must be greater than {DateTime.MinValue.Year}.");
        }
    }
}
