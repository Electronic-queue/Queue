using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.ReasonsForCancellations.Commands.CreateReasonsForCancellation;

public record CreateReasonsForCancellationCommand(
    int RecordId,
    string? Explantation
    ) :IRequest<Result>;
