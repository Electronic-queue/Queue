using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository _userRepository, ILogger<DeleteUserCommandHandler> _logger) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        
            _logger.LogInformation("Запрос на удалениие пользователя");
            var entity = await _userRepository.DeleteAsync(request.Id);
            if (entity.IsFailed)
            {
                return Result.Failure(new Error(Errors.BadRequest, "DeleteError"));
            }
            return Result.Success(entity);
            
            
        
        
    }
}
