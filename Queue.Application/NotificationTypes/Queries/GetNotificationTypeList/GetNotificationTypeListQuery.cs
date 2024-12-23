using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeList;

public class GetNotificationTypeListQuery : IRequest<Result<List<NotificationType>>>
{
}
