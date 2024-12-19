using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(int UserId,
    string FirstName,
    string LastName,
    string? Surname,
    string Login,
    string PasswordHash,
    bool IsDeleted) : IRequest<Result>
{
   
}
