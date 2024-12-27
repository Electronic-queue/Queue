namespace Queue.WebApi.Contracts.QueueTypeContracts;

public record CreateQueueTypeDto(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int? CreatedBy
    );

