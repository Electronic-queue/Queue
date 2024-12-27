namespace Queue.WebApi.Contracts.RecordStatusContracts;

public record UpdateRecordStatusDto(
     int RecordStatusId,
     string NameRu,
     string NameKk,
     string NameEn,
     string? DescriptionRu,
     string? DescriptionKk,
     string? DescriptionEn,
     int? CreatedBy);

