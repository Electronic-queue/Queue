using FluentValidation;

namespace Queue.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommandValidator:AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.RecordId)
           .GreaterThan(0).WithMessage("RecordId должен быть больше нуля.")
           .NotEmpty().WithMessage("RecordId обязательно.");
        RuleFor(x => x.Rating)
            .GreaterThan(0).WithMessage("Rating должен быть больше нуля.")
            .NotEmpty().WithMessage("Rating обязательно");
        RuleFor(x => x.Content)
            .MaximumLength(500).WithMessage("ContentEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.Content));
    }
}
