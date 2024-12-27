namespace Queue.WebApi.Contracts.QueueTypeContracts;

public record UpdateQueueTypeDto(
    int QueueTypeId,
     string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn
    );
