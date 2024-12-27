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
        _logger.LogInformation("Запрос на удалениие userWindow");
        var result = await userWIndowRepository.DeleteAsync(request.UserWindowId);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении userWindow с id: {request.UserWindowId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления userWindow"));
        }
        _logger.LogInformation($"Успешное удаление userWindow с id: {request.UserWindowId}.");
        return Result.Success();
    }
}
