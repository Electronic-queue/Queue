using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Services.Commands.UpdateService;

public record UpdateServiceCommand(
    int ServiceId,
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int AverageExecutionTime,
    int QueueTypeId,
    int? ParentserviceId) : IRequest<Result>;