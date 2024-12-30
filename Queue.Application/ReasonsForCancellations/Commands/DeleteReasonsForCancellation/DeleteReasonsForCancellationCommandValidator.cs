using FluentValidation;

namespace Queue.Application.ReasonsForCancellations.Commands.DeleteReasonsForCancellation;

public class DeleteReasonsForCancellationCommandValidator:AbstractValidator<DeleteReasonsForCancellationCommand>
{
    public DeleteReasonsForCancellationCommandValidator()
    {
        RuleFor(x => x.ReasonId)
          .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
          .NotEmpty().WithMessage("WindowId обязательно.");
    }
}
