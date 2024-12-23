using FluentValidation;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeList;

public class GetNotificationTypeListQueryValidator : AbstractValidator<GetNotificationTypeListQuery>
{
    public GetNotificationTypeListQueryValidator()
    {
    }
}
