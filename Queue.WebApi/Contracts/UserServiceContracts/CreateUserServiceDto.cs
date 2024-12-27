namespace Queue.WebApi.Contracts.UserServiceContracts;

public record CreateUserServiceDto(
    int UserId ,
    int ServiceId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int? CreatedBy
    );
