using FluentValidation;

namespace Queue.Application.Notifications.Queries.GetNotificationById;

public class GetNotificationByIdQueryValidator : AbstractValidator<GetNotificationByIdQuery>
{
    public GetNotificationByIdQueryValidator()
    {
        RuleFor(x => x.NotificationId)
            .GreaterThan(0).WithMessage("NotificationId должен быть больше нуля.");
    }
}
