using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;

public class GetNotificationTypeDetailsQuery : IRequest<Result>
{
    public int NotificationTypeId { get; set; }
}
