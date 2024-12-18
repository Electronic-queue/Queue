 using System.ComponentModel.DataAnnotations;
namespace Queue.WebApi.Models;

public record CreateUserDto([Required]string Iin,string FirstName,string LastName);

