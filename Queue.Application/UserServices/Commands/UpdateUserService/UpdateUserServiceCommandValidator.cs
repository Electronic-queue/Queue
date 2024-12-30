using FluentValidation;

namespace Queue.Application.UserServices.Commands.UpdateUserService;

public class UpdateUserServiceCommandValidator:AbstractValidator<UpdateUserServiceCommand>
{
    public UpdateUserServiceCommandValidator()
    {
        RuleFor(x => x.UserServiceId)
            .GreaterThan(0).WithMessage("UserServiceId должен быть больше нуля.")
            .NotEmpty().WithMessage("UserServiceId обязательно.");
        RuleFor(x => x.UserId)
         .GreaterThan(0).
         When(x => x.UserId.HasValue).WithMessage("WindowId должен быть больше нуля.");
        RuleFor(x => x.ServiceId)
         .GreaterThan(0).
         When(x => x.ServiceId.HasValue).WithMessage("WindowId должен быть больше нуля.");
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
