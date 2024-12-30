using FluentValidation;

namespace Queue.Application.UserServices.Commands.DeleteUserService;

public class DeleteUserServiceCommandValidator:AbstractValidator<DeleteUserServiceCommand>
{
    public DeleteUserServiceCommandValidator()
    {
        RuleFor(x => x.UserServiceId)
            .GreaterThan(0).WithMessage("UserServiceId должен быть больше нуля.")
            .NotEmpty().WithMessage("UserServiceId обязательно.");
    }
}
