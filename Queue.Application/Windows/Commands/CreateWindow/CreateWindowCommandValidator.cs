using FluentValidation;

namespace Queue.Application.Windows.Commands.CreateWindow;

public class CreateWindowCommandValidator:AbstractValidator<CreateWindowCommand>
{
    public CreateWindowCommandValidator()
    {
        RuleFor(x => x.WindowNumber)
            .GreaterThan(0).WithMessage("Rating должен быть больше нуля.")
            .NotEmpty().WithMessage("Rating обязательно");
        RuleFor(x => x.WindowStatusId)
            .GreaterThan(0).WithMessage("Rating должен быть больше нуля.")
            .NotEmpty().WithMessage("Rating обязательно");
        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy должен быть больше нуля.")
            .NotEmpty().WithMessage("CreatedBy обязательно.");
    }
}
