using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id, string Iin, string FirstName, string LastName) : IRequest<Result>
{
   
}
