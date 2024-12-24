using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.NotificationTypes.Commands.CreateNotificationType;

public record CreateNotificationTypeCommand(
    string NameRu="",
    string NameKk = "",
    string NameEn = "",
    string? DescriptionRu=null,
    string? DescriptionKk=null,
    string? DescriptionEn=null,
    int? CreatedBy=null) : IRequest<Result>;
