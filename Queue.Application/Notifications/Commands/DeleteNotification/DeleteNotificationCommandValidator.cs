using FluentValidation;

namespace Queue.Application.Notifications.Commands.DeleteNotification;

public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
{
    public DeleteNotificationCommandValidator()
    {
        RuleFor(x => x.NotificationId)
            .GreaterThan(0).WithMessage("NotificationId должен быть больше нуля.");
    }
}
