using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.WebApi.Contracts.NotificationTypeContracts;

public record UpdateNotificationTypeDto(
    int NotificationTypeId,
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn);


