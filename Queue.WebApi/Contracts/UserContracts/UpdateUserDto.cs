namespace Queue.WebApi.Contracts.UserContracts;

public record UpdateUsereDto(int UserId,
    string FirstName,
    string LastName,
    string? Surname,
    string Login,
    string PasswordHash,
    bool IsDeleted)
{

}


