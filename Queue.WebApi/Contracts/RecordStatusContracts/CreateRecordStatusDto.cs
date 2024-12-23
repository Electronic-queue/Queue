namespace Queue.WebApi.Contracts.RecordStatusContracts;

public record CreateRecordStatusDto(string NameRu,
     string NameKk,
     string NameEn,
     string? DescriptionRu,
     string? DescriptionKk,
     string? DescriptionEn,
     int? CreatedBy);
