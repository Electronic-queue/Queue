using FluentValidation;
using Queue.Application.WindowStatuses.Commands.UpdateWIndowStatus;

namespace Queue.Application.WindowStatuses.Commands.UpdateWindowStatus;

public class UpdateWindowStatusCommandValidator:AbstractValidator<UpdateWindowStatusCommand>
{
    public UpdateWindowStatusCommandValidator()
    {
        RuleFor(x => x.WindowStatusId)
           .GreaterThan(0).WithMessage("WindowStatusId должен быть больше нуля.")
           .NotEmpty().WithMessage("WindowStatusId обязательно.");

        RuleFor(x => x.NameRu)
            .MaximumLength(100).WithMessage("NameRu не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.NameRu));
        RuleFor(x => x.NameKk)
            .MaximumLength(100).WithMessage("NameKk не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.NameKk));
        RuleFor(x => x.NameEn)
            .MaximumLength(100).WithMessage("NameEn не должно превышать 100 символов.")
            .When(x => !string.IsNullOrEmpty(x.NameEn));

        RuleFor(x => x.DescriptionRu)
            .MaximumLength(500).WithMessage("DescriptionRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionRu));

        RuleFor(x => x.DescriptionKk)
            .MaximumLength(500).WithMessage("DescriptionKk не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionKk));

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(500).WithMessage("DescriptionEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionEn));
    }
}

