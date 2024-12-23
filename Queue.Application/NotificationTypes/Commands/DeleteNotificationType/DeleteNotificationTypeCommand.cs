using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.NotificationTypes.Commands.DeleteNotificationType;

public record DeleteNotificationTypeCommand(int NotificationTypeId) :IRequest<Result>;
