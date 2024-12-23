using KDS.Primitives.FluentResult;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Queue.WebApi.Contracts.NotificationContracts;

public record CreateNotificationDto(
    string NameRu,
    string NameKk,
    string NameEn,
    string? ContentRu,
    string? ContentKk,
    string? ContentEn,
    int NotificationTypeId,
    int? CreatedBy);

