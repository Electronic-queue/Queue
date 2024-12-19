namespace Queue.WebApi.Models;

public record UpdateUsereDto(int UserId,
    string FirstName,
    string LastName,
    string? Surname,
    string Login,
    string PasswordHash,
    bool IsDeleted)
{

}


