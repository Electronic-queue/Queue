namespace Queue.WebApi.Contracts.ExceedingsTimeContracts;

public record CreateExceedingsTimeDto(
    int WindowId,
    int TimeForExcommunication
    );
