using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeById;

public record GetExceedingsTimeByIdQuery(
    int ExceedingsTimeId
    ):IRequest<Result>;
