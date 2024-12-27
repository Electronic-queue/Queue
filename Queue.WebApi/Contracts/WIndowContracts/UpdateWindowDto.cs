namespace Queue.WebApi.Contracts.WIndowContracts;

public record UpdateWindowDto(
    int WindowId,
int WindowNumber,
int WindowStatusId,
    int CreatedBy);
