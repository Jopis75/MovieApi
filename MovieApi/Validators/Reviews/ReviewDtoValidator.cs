using FluentValidation;
using MovieApi.Dtos.Reviews;

namespace MovieApi.Validators.Reviews
{
    public class ReviewDtoValidator : AbstractValidator<ReviewDto>
    {
        public ReviewDtoValidator()
        {
            RuleFor(reviewDto => reviewDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(reviewDto => reviewDto.ReviewerName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(reviewDto => reviewDto.Comment)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(reviewDto => reviewDto.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("{PropertyName} must be between 1 and 5.");
        }
    }
}
