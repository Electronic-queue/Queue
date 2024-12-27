using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.WindowStatuses.Commands.DeletWindowStatus;

public record DeleteWindowStatusCommand(int WindowStatusId):IRequest<Result>;
