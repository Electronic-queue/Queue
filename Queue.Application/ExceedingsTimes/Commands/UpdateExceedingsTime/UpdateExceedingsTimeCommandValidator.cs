using FluentValidation;

namespace Queue.Application.ExceedingsTimes.Commands.UpdateExceedingsTime;

public class UpdateExceedingsTimeCommandValidator : AbstractValidator<UpdateExceedingsTimeCommand>
{
    public UpdateExceedingsTimeCommandValidator()
    {
        RuleFor(x => x.ExceedingsTimeId)
           .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
           .NotEmpty().WithMessage("WindowId обязательно.");
        RuleFor(x => x.WindowId)
          .GreaterThan(0).
          When(x => x.WindowId.HasValue).WithMessage("WindowId должен быть больше нуля.");
        RuleFor(x => x.TimeForExcommunication)
            .GreaterThan(0).
            When(x => x.TimeForExcommunication.HasValue).WithMessage("TimeForExcommunication должен быть больше нуля.");

    }
}
