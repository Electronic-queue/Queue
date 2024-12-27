using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeList;

public record GetExceedingsTimeListQuery():IRequest<Result<List<ExceedingsTime>>>;

