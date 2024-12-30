using FluentValidation;

namespace Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeById;

public class GetExceedingsTimeByIdQueryValidator:AbstractValidator<GetExceedingsTimeByIdQuery>
{
    public GetExceedingsTimeByIdQueryValidator()
    {
        RuleFor(x => x.ExceedingsTimeId)
          .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
          .NotEmpty().WithMessage("WindowId обязательно.");
    }
}
