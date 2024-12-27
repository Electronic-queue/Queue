using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.WindowStatuses.Queries.GetWindowStatusList;

public record GetWindowStatusListQuery():IRequest<Result<List<WindowStatus>>>;

