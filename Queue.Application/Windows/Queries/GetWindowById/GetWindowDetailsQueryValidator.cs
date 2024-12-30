using FluentValidation;
using Queue.Application.Windows.Queries.GetWindowDetails;

namespace Queue.Application.Windows.Queries.GetWindowById;

public class GetWindowDetailsQueryValidator:AbstractValidator<GetWindowDetailsQuery>
{
    public GetWindowDetailsQueryValidator()
    {
        RuleFor(x => x.WindowId)
            .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
            .NotEmpty().WithMessage("WindowId обязательно.");
    }
}
