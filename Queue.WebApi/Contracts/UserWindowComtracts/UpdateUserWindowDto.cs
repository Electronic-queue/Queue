namespace Queue.WebApi.Contracts.UserWindowComtracts;

public record UpdateUserWindowDto(
    int UserWindowId,
    int? UserId,
    int? WindowId
    );
