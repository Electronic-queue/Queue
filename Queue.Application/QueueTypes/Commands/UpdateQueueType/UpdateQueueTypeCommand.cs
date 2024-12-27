using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.QueueTypes.Commands.UpdateQueueType;

public record UpdateQueueTypeCommand(
    int QueueTypeId,
    string NameRu ,
    string NameKk ,
    string NameEn,
    string? DescriptionRu ,
    string? DescriptionKk ,
    string? DescriptionEn 
    ):IRequest<Result>;
