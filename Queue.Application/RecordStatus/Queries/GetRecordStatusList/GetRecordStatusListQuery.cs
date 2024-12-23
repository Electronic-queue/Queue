using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.RecordStatus.Queries.GetRecordStatusList;

public record GetRecordStatusListQuery():IRequest<Result<List<Domain.Entites.RecordStatus>>>;

