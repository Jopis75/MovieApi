using FluentValidation;
using MovieApi.Dtos.Reviews;

namespace MovieApi.Validators.Reviews
{
    public class ReadReviewDtoValidator : AbstractValidator<ReadReviewDto>
    {
        public ReadReviewDtoValidator()
        {
            RuleFor(readReviewDto => readReviewDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(readReviewDto => readReviewDto.ReviewerName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(readReviewDto => readReviewDto.Comment)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(readReviewDto => readReviewDto.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("{PropertyName} must be between 1 and 5.");

            RuleFor(readReviewDto => readReviewDto.MovieId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
