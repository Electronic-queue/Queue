using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Notifications.Commands.DeleteNotification;

public record DeleteNotificationCommand(int NotificationId) :IRequest<Result>;
