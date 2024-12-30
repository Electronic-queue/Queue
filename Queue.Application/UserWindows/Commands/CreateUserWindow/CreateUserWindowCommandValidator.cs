using FluentValidation;

namespace Queue.Application.UserWindows.Commands.CreateUserWindow;

public class CreateUserWindowCommandValidator:AbstractValidator<CreateUserWindowCommand>
{
    public CreateUserWindowCommandValidator()
    {
        RuleFor(x => x.UserId)
           .GreaterThan(0).WithMessage("UserId должен быть больше нуля.")
           .NotEmpty().WithMessage("UserId обязательно.");
        RuleFor(x => x.WindowId)
           .GreaterThan(0).WithMessage("ServiceId должен быть больше нуля.")
           .NotEmpty().WithMessage("ServiceId обязательно.");
        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy должен быть больше нуля.")
            .NotEmpty().WithMessage("CreatedBy обязательно.");
    }
}
