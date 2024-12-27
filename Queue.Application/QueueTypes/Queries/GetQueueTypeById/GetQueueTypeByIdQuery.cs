using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.QueueTypes.Queries.GetQueueTypeById;

public record GetQueueTypeByIdQuery(int QueueTypeId):IRequest<Result>;
