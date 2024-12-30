using FluentValidation;

namespace Queue.Application.UserServices.Commands.CreateUserService;

public class CreateUserServiceCommandValidator:AbstractValidator<CreateUserServiceCommand>
{
    public CreateUserServiceCommandValidator()
    {
        RuleFor(x => x.UserId)
           .GreaterThan(0).WithMessage("UserId должен быть больше нуля.")
           .NotEmpty().WithMessage("UserId обязательно.");
        RuleFor(x => x.ServiceId)
           .GreaterThan(0).WithMessage("ServiceId должен быть больше нуля.")
           .NotEmpty().WithMessage("ServiceId обязательно.");
        RuleFor(x => x.DescriptionRu)
            .MaximumLength(500).WithMessage("DescriptionRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionRu));

        RuleFor(x => x.DescriptionKk)
            .MaximumLength(500).WithMessage("DescriptionKk не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionKk));

        RuleFor(x => x.DescriptionEn)
            .MaximumLength(500).WithMessage("DescriptionEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.DescriptionEn));

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy должен быть больше нуля.")
            .NotEmpty().WithMessage("CreatedBy обязательно.");
    }
}
