using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int UserId):IRequest<Result>;
}
