using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserWindows.Commands.DeleteUserWindow;

public class DeleteUserWindowCommandHandler(IUserWindowRepository userWIndowRepository, ILogger<DeleteUserWindowCommandHandler> _logger) : IRequestHandler<DeleteUserWindowCommand, Result>
{
    public async Task<Result> Handle(DeleteUserWindowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удаление UserWindow из базы данных.");
        var result = await userWIndowRepository.DeleteAsync(request.UserWindowId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление UserWindow из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
