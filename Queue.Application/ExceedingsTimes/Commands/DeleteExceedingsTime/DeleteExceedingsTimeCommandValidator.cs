using FluentValidation;

namespace Queue.Application.ExceedingsTimes.Commands.DeleteExceedingsTime;

public class DeleteExceedingsTimeCommandValidator:AbstractValidator<DeleteExceedingsTimeCommand>
{
	public DeleteExceedingsTimeCommandValidator()
	{
        RuleFor(x => x.ExceedingsTimeId)
           .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
           .NotEmpty().WithMessage("Id обязательно.");
    }
}
