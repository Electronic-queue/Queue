namespace Queue.WebApi.Contracts.RecordContracts;

public record UpdateRecordDto(
    int RecordId,
    string FirstName,
  string LastName,
  string? Surname,
  string Iin,
  int RecordStatusId,
  int ServiceId,
  bool IsCreatedByEmployee,
  int? CreatedBy,
  int TicketNumber);
