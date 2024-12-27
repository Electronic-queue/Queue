namespace Queue.WebApi.Contracts.WindowStatusContracts;

public record UpdateWindowStatusDto(
    int WindowStatusId,
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn
    );

