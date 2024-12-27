using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Notifications.Commands.DeleteNotification;

public class DeleteNotificationCommandHandler(INotificationRepository _notificationRepository, ILogger<DeleteNotificationCommandHandler> _logger) : IRequestHandler<DeleteNotificationCommand, Result>
{
    public async Task<Result> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие уведомления");
        var result = await _notificationRepository.DeleteAsync(request.NotificationId);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении уведомления с id: {request.NotificationId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления уведомления."));
        }
        _logger.LogInformation($"Успешное удаление уведомления с id: {request.NotificationId}.");
        return result;
    }
}
