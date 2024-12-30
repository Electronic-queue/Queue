using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.WindowStatuses.Commands.DeleteWindowStatus;

public class DeleteWindowStatusCommandHandler(IWindowStatusRepository windowStatusRepository, ILogger<DeleteWindowStatusCommandHandler> _logger) : IRequestHandler<DeleteWindowStatusCommand, Result>
{
    public async Task<Result> Handle(DeleteWindowStatusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удаление статуса окна из базы данных.");
        var result = await windowStatusRepository.DeleteAsync(request.WindowStatusId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление статуса окна из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();

    }

}
