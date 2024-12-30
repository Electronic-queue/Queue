using FluentValidation;

namespace Queue.Application.ReasonsForCancellations.Queries.ReasonsForCancellationById;

public class GetReasonsForCancellationByIdQueryValidator:AbstractValidator<GetReasonsForCancellationByIdQuery>
{
    public GetReasonsForCancellationByIdQueryValidator()
    {
        RuleFor(x => x.ReasonId)
         .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
         .NotEmpty().WithMessage("WindowId обязательно.");
    }
}
