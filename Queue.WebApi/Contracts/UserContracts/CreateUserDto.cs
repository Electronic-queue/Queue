using System.ComponentModel.DataAnnotations;
namespace Queue.WebApi.Contracts.UserContracts;

public record CreateUserDto(string FirstName,
    string LastName,
    string? Surname,
    string Login,
    string PasswordHash,
    int? CreatedBy);


