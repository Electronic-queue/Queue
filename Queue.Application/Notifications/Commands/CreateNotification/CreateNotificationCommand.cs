using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Notifications.Commands.CreateNotification;

public record CreateNotificationCommand(
    string NameRu,
    string NameKk,
    string NameEn,
    string? ContentRu,
    string? ContentKk,
    string? ContentEn,
    int NotificationTypeId,
    int? CreatedBy) : IRequest<Result>;