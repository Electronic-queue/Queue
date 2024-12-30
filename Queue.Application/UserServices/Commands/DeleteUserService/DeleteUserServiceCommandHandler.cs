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
        _logger.LogInformation("Обработка запроса на удаление UserService из базы данных.");
        var result = await userServiceRepository.DeleteAsync(request.UserServiceId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление UserService из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
