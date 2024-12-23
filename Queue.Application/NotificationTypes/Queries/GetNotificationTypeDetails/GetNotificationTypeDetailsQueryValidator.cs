using FluentValidation;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;

public class GetNotificationTypeDetailsQueryValidator : AbstractValidator<GetNotificationTypeDetailsQuery>
{
    public GetNotificationTypeDetailsQueryValidator()
    {
        RuleFor(x => x.NotificationTypeId)
            .GreaterThan(0).WithMessage("NotificationTypeId должен быть больше нуля.");
    }
}
