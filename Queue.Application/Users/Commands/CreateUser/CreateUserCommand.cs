using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Users.Commands.CreateUser;

public record CreateUserCommand(Guid Id,string Iin, string FirstName, string LastName) : IRequest<Result<Guid>>;

