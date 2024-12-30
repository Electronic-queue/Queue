using FluentValidation;

namespace Queue.Application.UserWindows.Commands.UpdateUserWindow;

public class UpdateUserWindowCommandValidator:AbstractValidator<UpdateUserWindowCommand>
{
    public UpdateUserWindowCommandValidator()
    {
        RuleFor(x => x.UserWindowId)
            .GreaterThan(0).WithMessage("UserWindowId должен быть больше нуля.")
            .NotEmpty().WithMessage("UserWindowId обязательно.");
             
        RuleFor(x => x.UserId)
         .GreaterThan(0).
         When(x => x.UserId.HasValue).WithMessage("WindowId должен быть больше нуля.");
             
        RuleFor(x => x.WindowId)
         .GreaterThan(0).
         When(x => x.WindowId.HasValue).WithMessage("WindowId должен быть больше нуля.");
    }
}
