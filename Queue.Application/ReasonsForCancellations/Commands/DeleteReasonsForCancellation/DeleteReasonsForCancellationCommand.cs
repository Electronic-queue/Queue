using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.ReasonsForCancellations.Commands.DeleteReasonsForCancellation;

public record DeleteReasonsForCancellationCommand(
    int ReasonId
    ):IRequest<Result>;
