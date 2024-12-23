using FluentValidation;

namespace Queue.Application.NotificationTypes.Commands.DeleteNotificationType;

public class DeleteNotificationTypeCommandValidator : AbstractValidator<DeleteNotificationTypeCommand>
{
    public DeleteNotificationTypeCommandValidator()
    {
        RuleFor(x => x.NotificationTypeId)
            .GreaterThan(0).WithMessage("NotificationTypeId должен быть больше нуля.");
    }
}
