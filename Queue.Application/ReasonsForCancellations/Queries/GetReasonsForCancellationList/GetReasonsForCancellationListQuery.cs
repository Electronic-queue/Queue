using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.ReasonsForCancellations.Queries.GetReasonsForCancellationList;

public record GetReasonsForCancellationListQuery():IRequest<Result<List<ReasonsForCancellation>>>;
