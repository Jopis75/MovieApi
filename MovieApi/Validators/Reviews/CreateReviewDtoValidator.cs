using FluentValidation;
using MovieApi.Dtos.Reviews;

namespace MovieApi.Validators.Reviews
{
    public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewDtoValidator()
        {
            RuleFor(createReviewDto => createReviewDto.ReviewerName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createReviewDto => createReviewDto.Comment)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createReviewDto => createReviewDto.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("{PropertyName} must be between 1 and 5.");

            RuleFor(createReviewDto => createReviewDto.MovieId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
