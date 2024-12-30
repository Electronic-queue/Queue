using FluentValidation;

namespace Queue.Application.Windows.Commands.DeleteWindow;

public class DeleteWindowCommandValidator:AbstractValidator<DeleteWindowCommand>
{
    public DeleteWindowCommandValidator()
    {
        RuleFor(x => x.WindowId)
            .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
            .NotEmpty().WithMessage("WindowId обязательно.");
    }
}
