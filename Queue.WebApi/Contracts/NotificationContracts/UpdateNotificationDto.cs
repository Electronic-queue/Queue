using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.WebApi.Contracts.NotificationContracts;

public record UpdateNotificationDto(
    int NotificationId,
    string NameRu,
    string NameKk,
    string NameEn,
    string? ContentRu,
    string? ContentKk,
    string? ContentEn,
    int NotificationTypeId);


