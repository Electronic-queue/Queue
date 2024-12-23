using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Record.Commands.DeleteRecord;

public record DeleteRecordCommand(int RecordId):IRequest<Result>;

