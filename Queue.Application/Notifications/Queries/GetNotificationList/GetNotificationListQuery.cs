using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Notifications.Queries.GetNotificationList;

public class GetNotificationListQuery : IRequest<Result<List<Notification>>>
{
}
