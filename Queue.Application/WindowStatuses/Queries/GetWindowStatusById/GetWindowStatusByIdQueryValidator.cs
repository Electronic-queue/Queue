using FluentValidation;

namespace Queue.Application.WindowStatuses.Queries.GetWindowStatusById;

public class GetWindowStatusByIdQueryValidator:AbstractValidator<GetWindowStatusByIdQuery>
{
    public GetWindowStatusByIdQueryValidator()
    {
        RuleFor(x => x.WindowStatusId)
            .GreaterThan(0).WithMessage("WindowStatusId должен быть больше нуля.")
            .NotEmpty().WithMessage("WindowStatusId обязательно.");
    }
}
