using FluentValidation;

namespace Queue.Application.Services.Queries.GetServiceById;

public class GetServiceByIdQueryValidator : AbstractValidator<GetServiceByIdQuery>
{
    public GetServiceByIdQueryValidator()
    {
        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("ServiceId должен быть больше нуля.")
            .NotEmpty().WithMessage("ServiceId обязательно.");
    }
}
