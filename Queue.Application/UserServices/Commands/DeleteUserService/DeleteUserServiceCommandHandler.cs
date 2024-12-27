using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserServices.Commands.DeleteUserService;

public class DeleteUserServiceCommandHandler(IUserServiceRepository userServiceRepository, ILogger<DeleteUserServiceCommandHandler> _logger) : IRequestHandler<DeleteUserServiceCommand, Result>
{
    public async Task<Result> Handle(DeleteUserServiceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие userService");
        var userService = await userServiceRepository.DeleteAsync(request.UserServiceId);
        if (userService.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении userService с id: {request.UserServiceId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления userService"));
        }
        _logger.LogInformation($"Успешное удаление userService с id: {request.UserServiceId}.");
        return Result.Success();
    }
}
