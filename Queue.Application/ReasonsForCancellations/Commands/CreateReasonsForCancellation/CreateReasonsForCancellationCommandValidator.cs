using FluentValidation;

namespace Queue.Application.ReasonsForCancellations.Commands.CreateReasonsForCancellation;

public class CreateReasonsForCancellationCommandValidator:AbstractValidator<CreateReasonsForCancellationCommand>
{
	public CreateReasonsForCancellationCommandValidator()
	{
        RuleFor(x => x.RecordId)
          .GreaterThan(0).WithMessage("WindowId должен быть больше нуля.")
          .NotEmpty().WithMessage("WindowId обязательно.");
        RuleFor(x => x.Explantation)
            .MaximumLength(500).WithMessage("DescriptionRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.Explantation));
    }
}
