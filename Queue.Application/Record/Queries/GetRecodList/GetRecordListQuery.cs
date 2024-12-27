using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Record.Queries.GetRecodList;

public record GetRecordListQuery():IRequest<Result<List<Domain.Entites.Record>>>;

