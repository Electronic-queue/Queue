namespace Queue.WebApi.Contracts.UserServiceContracts;

public record UpdateUserServiceDto(int UserServiceId,
    int? UserId,
    int? ServiceId,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    bool? IsActive
   
    );
