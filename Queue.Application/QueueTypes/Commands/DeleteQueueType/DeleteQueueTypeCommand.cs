using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.QueueTypes.Commands.DeleteQueueType;

public record DeleteQueueTypeCommand(int QueueTypeId):IRequest<Result>;
