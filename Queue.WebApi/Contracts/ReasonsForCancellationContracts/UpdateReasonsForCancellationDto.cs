namespace Queue.WebApi.Contracts.ReasonsForCancellationContracts;

public record UpdateReasonsForCancellationDto(
    int ReasonId,
    int? RecordId,
    string? Explantation
    );
