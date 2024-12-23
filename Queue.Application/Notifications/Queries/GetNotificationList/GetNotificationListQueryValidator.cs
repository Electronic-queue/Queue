using FluentValidation;

namespace Queue.Application.Notifications.Queries.GetNotificationList;

public class GetNotificationListQueryValidator : AbstractValidator<GetNotificationListQuery>
{
    public GetNotificationListQueryValidator()
    {
    }
}
