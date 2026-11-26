using FluentValidation;
using MovieApi.Dtos.Reviews;

namespace MovieApi.Validators.Reviews
{
    public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidator()
        {
            RuleFor(updateReviewDto => updateReviewDto.ReviewerName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateReviewDto => updateReviewDto.Comment)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateReviewDto => updateReviewDto.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("{PropertyName} must be between 1 and 5.");
        }
    }
}
