using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.UserWindows.Queries.GetUserWindowList;

public record GetUserWindowListQuery():IRequest<Result<List<UserWindow>>>;
