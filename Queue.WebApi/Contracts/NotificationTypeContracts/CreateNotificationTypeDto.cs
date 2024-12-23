using KDS.Primitives.FluentResult;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Queue.WebApi.Contracts.NotificationTypeContracts;

public record CreateNotificationTypeDto(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int? CreatedBy);

