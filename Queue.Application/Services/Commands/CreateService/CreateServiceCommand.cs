using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Services.Commands.CreateService;

public record CreateServiceCommand(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int AverageExecutionTime,
    int QueueTypeId,
    int? ParentserviceId,
    int? CreatedBy) : IRequest<Result>;