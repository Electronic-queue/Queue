namespace Queue.WebApi.Contracts.UserWindowComtracts;

public record CreateUserWindowDto(
    int UserId,
    int WindowId,
    int? CreatedBy
    );
