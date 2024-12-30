using FluentValidation;

namespace Queue.Application.Windows.Commands.UpdateWindow;

public class UpdateWindowCommandValidator:AbstractValidator<UpdateWindowCommand>
{
    public UpdateWindowCommandValidator()
    {
        RuleFor(x => x.WindowId)
            .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
            .NotEmpty().WithMessage("WindowId обязательно.");
        RuleFor(x => x.WindowNumber)
         .GreaterThan(0).
         When(x => x.WindowNumber.HasValue).WithMessage("WindowId должен быть больше нуля.");
        RuleFor(x => x.WindowStatusId)
         .GreaterThan(0).
         When(x => x.WindowStatusId.HasValue).WithMessage("WindowId должен быть больше нуля.");
    }
}
