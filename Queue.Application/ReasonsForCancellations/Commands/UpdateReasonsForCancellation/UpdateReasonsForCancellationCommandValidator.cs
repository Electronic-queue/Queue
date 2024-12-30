using FluentValidation;

namespace Queue.Application.ReasonsForCancellations.Commands.UpdateReasonsForCancellation;

public class UpdateReasonsForCancellationCommandValidator:AbstractValidator<UpdateReasonsForCancellationCommand>
{
	public UpdateReasonsForCancellationCommandValidator()
	{
        RuleFor(x => x.ReasonId)
         .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
         .NotEmpty().WithMessage("WindowId обязательно.");
        RuleFor(x => x.RecordId)
          .GreaterThan(0).When(x => x.RecordId.HasValue).WithMessage("WindowId должен быть больше нуля.");
        RuleFor(x => x.Explantation)
            .MaximumLength(500).WithMessage("DescriptionRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.Explantation));
    }
}
