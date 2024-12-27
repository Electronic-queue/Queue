using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.ReasonsForCancellations.Queries.ReasonsForCancellationById;

public record GetReasonsForCancellationByIdQuery(
    int ReasonId
    ):IRequest<Result>;

