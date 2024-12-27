namespace Queue.WebApi.Contracts.WindowStatusContracts;

public record CreateWindowStatusDto(
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int? CreatedBy
    );
