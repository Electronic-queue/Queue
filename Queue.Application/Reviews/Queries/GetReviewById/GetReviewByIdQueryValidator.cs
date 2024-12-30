using FluentValidation;

namespace Queue.Application.Reviews.Queries.GetReviewById;

public class GetReviewByIdQueryValidator:AbstractValidator<GetReviewByIdQuery>
{
	public GetReviewByIdQueryValidator()
	{
        RuleFor(x => x.ReviewId)
            .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
            .NotEmpty().WithMessage("Id обязательно.");
    }
}
