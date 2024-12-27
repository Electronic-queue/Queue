using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.WindowStatuses.Commands.DeletWindowStatus;

public class DeleteWindowStatusCommandHandler(IWindowStatusRepository windowStatusRepository, ILogger<DeleteWindowStatusCommandHandler> _logger) : IRequestHandler<DeleteWindowStatusCommand, Result>
{
    public async Task<Result> Handle(DeleteWindowStatusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие статуса окна");
        var windowStatus = await windowStatusRepository.DeleteAsync(request.WindowStatusId);
        if (windowStatus.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении статуса окна с id: {request.WindowStatusId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления статуса окна"));
        }
        _logger.LogInformation($"Успешное удаление статуса окна с id: {request.WindowStatusId}.");
        return Result.Success();

    }

}
