using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.WindowStatuses.Commands.DeleteWindowStatus;

public record DeleteWindowStatusCommand(int WindowStatusId):IRequest<Result>;
