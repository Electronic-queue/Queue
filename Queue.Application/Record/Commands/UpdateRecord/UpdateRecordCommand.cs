using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Record.Commands.UpdateRecord;

public record UpdateRecordCommand(int RecordId,
    string? FirstName=null,
  string? LastName = null,
  string? Surname = null,
  string? Iin=null,
  int? RecordStatusId=null,
  int? ServiceId=null,
  bool? IsCreatedByEmployee = null,
  int? CreatedBy=null,
  int? TicketNumber = null
    ) :IRequest<Result>;

