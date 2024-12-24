using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Services.Commands.CreateService;

public record CreateServiceCommand(
    string NameRu="",
    string NameKk = "",
    string NameEn = "",
    string? DescriptionRu=null,
    string? DescriptionKk=null,
    string? DescriptionEn = null,
    int AverageExecutionTime=0,
    int QueueTypeId = 0,
    int? ParentserviceId=null,
    int? CreatedBy=null) : IRequest<Result>;