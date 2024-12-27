namespace Queue.WebApi.Contracts.ReasonsForCancellationContracts;

public record CreateReasonsForCancellationDto(
    int RecordId,
    string? Explantation
    );
