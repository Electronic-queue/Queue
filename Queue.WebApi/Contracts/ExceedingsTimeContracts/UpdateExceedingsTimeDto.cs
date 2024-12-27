namespace Queue.WebApi.Contracts.ExceedingsTimeContracts;

public record UpdateExceedingsTimeDto(
    int ExceedingsTimeId,
    int? WindowId ,
    int? TimeForExcommunication,
    DateTime? CanceledOn 
    );
