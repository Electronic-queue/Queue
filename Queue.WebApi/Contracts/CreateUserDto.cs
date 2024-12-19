 using System.ComponentModel.DataAnnotations;
namespace Queue.WebApi.Models;

public record CreateUserDto(string Iin,string? FirstName,string? LastName);

