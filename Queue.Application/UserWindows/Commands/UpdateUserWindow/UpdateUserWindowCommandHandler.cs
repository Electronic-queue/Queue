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
        _logger.LogInformation("Запрос на обновление UserWindow");
        var userWindow = await userWIndowRepository.UpdateAsync(
            userWindowId: request.UserWindowId,
            windowId: request.WindowId,
            userId: request.UserId
            ) ;
        if( userWindow.IsFailed) 
        {
            _logger.LogError($"Ошибка при обновлении userWindow с id: {request.UserWindowId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления userWindow"));
        }
        _logger.LogInformation($"Успешное обновление userWindow с id: {request.UserWindowId}.");
        return Result.Success();

    }
}
