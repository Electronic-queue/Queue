using FluentValidation;

namespace Queue.Application.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandValidator:AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
        RuleFor(x => x.RecordId)
           .GreaterThan(0).When(x => x.RecordId.HasValue).WithMessage("RecordId должен быть больше нуля.");
        RuleFor(x => x.Rating)
            .GreaterThan(0).When(x => x.Rating.HasValue).WithMessage("Rating должен быть больше нуля.");    
        RuleFor(x => x.Content)
            .MaximumLength(500).WithMessage("ContentEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.Content));
    }
}
