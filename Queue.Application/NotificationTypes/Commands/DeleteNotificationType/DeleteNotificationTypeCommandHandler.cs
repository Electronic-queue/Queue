using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using Serilog.Core;

namespace Queue.Application.NotificationTypes.Commands.DeleteNotificationType;

public class DeleteNotificationTypeCommandHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<DeleteNotificationTypeCommandHandler> _logger) : IRequestHandler<DeleteNotificationTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteNotificationTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удалениие типа уведомления из базы данных.");
        var result = await _notificationTypeRepository.DeleteAsync(request.NotificationTypeId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление типа уведомления из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }

        _logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
