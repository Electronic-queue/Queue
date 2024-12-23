using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Record.Commands.CreateRecord;

public record CreateRecordCommand(
    string FirstName = "",
    string LastName = "",
    string? Surname = null,
    string Iin = "0",
    int RecordStatusId = 0,
    int ServiceId = 0,
    DateTime? StartTime = null,
    DateTime? EndTime = null,
    bool IsCreatedByEmployee = false,
    int? CreatedBy = null,
    int TicketNumber = 0
) : IRequest<Result>;
