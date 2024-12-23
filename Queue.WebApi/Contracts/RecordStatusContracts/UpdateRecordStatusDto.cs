namespace Queue.WebApi.Contracts.RecordStatusContracts;

public record UpdateRecordStatusDto(
    int RecordId,
    string NameRu,
     string NameKk,
     string NameEn,
     string? DescriptionRu,
     string? DescriptionKk,
     string? DescriptionEn,
     int? CreatedBy);

