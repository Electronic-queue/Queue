using FluentValidation;

namespace Queue.Application.UserServices.Queries.GetUserServiceById;

public class GetUserServiceByIdQueryValidator:AbstractValidator<GetUserServiceByIdQuery>
{
    public GetUserServiceByIdQueryValidator()
    {
        RuleFor(x => x.UserServiceId)
            .GreaterThan(0).WithMessage("UserServiceId должен быть больше нуля.")
            .NotEmpty().WithMessage("UserServiceId обязательно.");
    }
}
