using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Record.Queries.GetRecordById;

public record GetRecordByIdQuery(int RecordId):IRequest<Result>;
