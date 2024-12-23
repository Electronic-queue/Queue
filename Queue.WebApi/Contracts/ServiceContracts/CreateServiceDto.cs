namespace Queue.WebApi.Contracts.ServiceContracts;

public record CreateServiceDto(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int AverageExecutionTime,
    int QueueTypeId,
    int? ParentserviceId,
    int? CreatedBy);

