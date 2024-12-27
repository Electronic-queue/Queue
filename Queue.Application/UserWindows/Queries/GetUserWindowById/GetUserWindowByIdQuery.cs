using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserWindows.Queries.GetUserWindowById;

public record GetUserWindowByIdQuery(
    int UserWindowId
    ):IRequest<Result>;
