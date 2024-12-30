using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Notifications.Queries.GetNotificationById;

public record GetNotificationByIdQuery(int NotificationId) : IRequest<Result<Notification>>;

