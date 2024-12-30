using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserWindows.Commands.UpdateUserWindow;

public class UpdateUserWindowCommandHandler(IUserWindowRepository userWIndowRepository,ILogger<UpdateUserWindowCommandHandler> _logger) : IRequestHandler<UpdateUserWindowCommand, Result>
{
    public async Task<Result> Handle(UpdateUserWindowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление UserWindow в базе данных.");
        var result = await userWIndowRepository.UpdateAsync(
            userWindowId: request.UserWindowId,
            windowId: request.WindowId,
            userId: request.UserId
            ) ;
        if(result.IsFailed) 
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление UserWindow в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();

    }
}
