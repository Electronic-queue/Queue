using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.QueueTypes.Commands.CreateQueueType;

public record CreateQueueTypeCommand(
    string NameRu="0",
    string NameKk="0",
    string NameEn="0",
    string? DescriptionRu=null,
    string? DescriptionKk = null,
    string? DescriptionEn = null,
    DateTime? CreatedOn=null,
    int? CreatedBy=null
    ) :IRequest<Result>;
