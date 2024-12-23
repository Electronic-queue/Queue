using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Notifications.Commands.UpdateNotification;

public record UpdateNotificationCommand(
    int NotificationId,
    string NameRu,
    string NameKk,
    string NameEn,
    string? ContentRu,
    string? ContentKk,
    string? ContentEn,
    int NotificationTypeId) : IRequest<Result>;