using FluentValidation;

namespace Queue.Application.Notifications.Commands.UpdateNotification;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(x => x.NotificationId)
            .GreaterThan(0).WithMessage("NotificationId должен быть больше нуля.");

        RuleFor(x => x.NameRu)
            .MaximumLength(100).WithMessage("NameRu не должно превышать 100 символов.")
            .NotEmpty().WithMessage("NameRu обязательно.");

        RuleFor(x => x.NameKk)
            .MaximumLength(100).WithMessage("NameKk не должно превышать 100 символов.")
            .NotEmpty().WithMessage("NameKk обязательно.");

        RuleFor(x => x.NameEn)
            .MaximumLength(100).WithMessage("NameKk не должно превышать 100 символов.")
            .NotEmpty().WithMessage("NameKk обязательно.");

        RuleFor(x => x.ContentRu)
            .MaximumLength(500).WithMessage("ContentRu не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.ContentRu));

        RuleFor(x => x.ContentKk)
            .MaximumLength(500).WithMessage("ContentKk не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.ContentKk));

        RuleFor(x => x.ContentEn)
            .MaximumLength(500).WithMessage("ContentEn не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.ContentEn));

        RuleFor(x => x.NotificationTypeId)
            .GreaterThan(0).WithMessage("NotificationTypeId должен быть больше нуля.");
    }
}
