using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Windows.Commands.DeleteWindow;

public record DeleteWindowCommandHandler(IWindowRepository windowRepository, ILogger<DeleteWindowCommandHandler> _logger) :IRequestHandler<DeleteWindowCommand,Result>
{
    public async Task<Result> Handle(DeleteWindowCommand request,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удаление окна из базы данных.");
        var result = await windowRepository.DeleteAsync(request.WindowId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление окна из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
