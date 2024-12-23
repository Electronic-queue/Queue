using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Notifications.Queries.GetNotificationById;

public class GetNotificationByIdQuery : IRequest<Result<Notification>>
{
    public int NotificationId { get; set; }
}
