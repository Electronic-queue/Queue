using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Record.Commands.UpdateRecord;

public record UpdateRecordCommand(int RecordId,
    string FirstName,
  string LastName,
  string? Surname,
  string Iin,
  int RecordStatusId,
  int ServiceId,
  bool IsCreatedByEmployee,
  int? CreatedBy,
  int TicketNumber
    ) :IRequest<Result>;

