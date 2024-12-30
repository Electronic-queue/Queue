using FluentValidation;

namespace Queue.Application.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandValidator:AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
