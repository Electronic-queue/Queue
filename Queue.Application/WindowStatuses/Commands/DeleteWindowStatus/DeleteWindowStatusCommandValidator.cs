using FluentValidation;

namespace Queue.Application.WindowStatuses.Commands.DeleteWindowStatus;

public class DeleteWindowStatusCommandValidator:AbstractValidator<DeleteWindowStatusCommand>
{
    public DeleteWindowStatusCommandValidator()
    {
        RuleFor(x => x.WindowStatusId)
            .GreaterThan(0).WithMessage("WindowStatusId должен быть больше нуля.")
            .NotEmpty().WithMessage("WindowStatusId обязательно.");
    }
}
