using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Notifications.Commands.CreateNotification;

public record CreateNotificationCommand(
    string NameRu="",
    string NameKk = "",
    string NameEn = "",
    string? ContentRu=null,
    string? ContentKk = null,
    string? ContentEn=null,
    int NotificationTypeId=0,
    int? CreatedBy = null) : IRequest<Result>;