namespace Queue.WebApi.Contracts.WIndowContracts;

public record CreateWindowDto(
     int WindowNumber,
     int WindowStatusId,
     int CreatedBy
     );
