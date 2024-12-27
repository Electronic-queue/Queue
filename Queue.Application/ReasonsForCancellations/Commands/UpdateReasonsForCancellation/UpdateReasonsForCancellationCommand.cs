using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.ReasonsForCancellations.Commands.UpdateReasonsForCancellation;

public record UpdateReasonsForCancellationCommand(
    int ReasonId,
    int? RecordId=null,
    string? Explantation=null
    ) :IRequest<Result>;

