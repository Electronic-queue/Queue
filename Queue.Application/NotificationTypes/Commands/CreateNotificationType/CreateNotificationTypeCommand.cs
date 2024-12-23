using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.NotificationTypes.Commands.CreateNotificationType;

public record CreateNotificationTypeCommand(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int? CreatedBy) : IRequest<Result>;
