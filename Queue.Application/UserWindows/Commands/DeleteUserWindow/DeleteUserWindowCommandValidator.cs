using FluentValidation;

namespace Queue.Application.UserWindows.Commands.DeleteUserWindow;

public class DeleteUserWindowCommandValidator:AbstractValidator<DeleteUserWindowCommand>
{
    public DeleteUserWindowCommandValidator()
    {
        RuleFor(x => x.UserWindowId)
            .GreaterThan(0).WithMessage("UserWindowId должен быть больше нуля.")
            .NotEmpty().WithMessage("UserWindowId обязательно.");
    }
}
