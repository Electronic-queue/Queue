using FluentValidation;

namespace Queue.Application.ExceedingsTimes.Commands.CreateExceedingsTime;

public class CreateExceedingsTimeCommandValidator:AbstractValidator<CreateExceedingsTimeCommand>
{
	public CreateExceedingsTimeCommandValidator()
	{
        RuleFor(x => x.WindowId)
           .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
           .NotEmpty().WithMessage("WindowId обязательно.");
        RuleFor(x => x.TimeForExcommunication)
            .GreaterThan(0).WithMessage("TimeForExcommunication должен быть больше нуля.")
            .NotEmpty().WithMessage("TimeForExcommunication обязательно");
           

    }
}
