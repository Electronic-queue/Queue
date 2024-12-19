using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;


namespace Queue.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository _userRepository, ILogger<UpdateUserCommandHandler> _logger) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
    var user = new User
    {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Surname = request.Surname,
        Login = request.Login,
        PasswordHash = request.PasswordHash,
        IsDeleted = request.IsDeleted,
    };

    var entity = await _userRepository.UpdateAsync(user);

    if(entity.IsFailed) 
    {
        return Result.Failure(new Error(Errors.BadRequest, "UpdateError"));
    }
    return Result.Success(entity);
    }
}
