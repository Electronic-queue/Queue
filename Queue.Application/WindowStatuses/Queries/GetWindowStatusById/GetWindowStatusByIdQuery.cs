using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.WindowStatuses.Queries.GetWindowStatusById;

public record GetWindowStatusByIdQuery(
    int WindowStatusId
):IRequest<Result>;
