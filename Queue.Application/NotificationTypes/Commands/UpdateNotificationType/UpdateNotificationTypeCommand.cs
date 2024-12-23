using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.NotificationTypes.Commands.UpdateNotificationType;

public record UpdateNotificationTypeCommand(
    int NotificationTypeId,
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn) : IRequest<Result>;
