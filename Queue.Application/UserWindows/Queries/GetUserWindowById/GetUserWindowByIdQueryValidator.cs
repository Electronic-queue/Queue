using FluentValidation;

namespace Queue.Application.UserWindows.Queries.GetUserWindowById;

public class GetUserWindowByIdQueryValidator:AbstractValidator<GetUserWindowByIdQuery>
{
    public GetUserWindowByIdQueryValidator()
    {
        RuleFor(x => x.UserWindowId)
            .GreaterThan(0).WithMessage("UserWindowId должен быть больше нуля.")
            .NotEmpty().WithMessage("UserWindowId обязательно.");
    }
}
