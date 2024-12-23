using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.NotificationTypes.Commands.DeleteNotificationType;

public class DeleteNotificationTypeCommandHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<DeleteNotificationTypeCommandHandler> _logger) : IRequestHandler<DeleteNotificationTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteNotificationTypeCommand request, CancellationToken cancellationToken)
    {
        var result = await _notificationTypeRepository.DeleteAsync(request.NotificationTypeId);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении типа уведомлений с id: {request.NotificationTypeId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления типа уведомления."));
        }
        _logger.LogInformation($"Успешное удаление типа уведомления с id: {request.NotificationTypeId}.");
        return result;
    }
}
