using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;


namespace Queue.Application.Notifications.Commands.UpdateNotification;

public class UpdateNotificationCommandHandler(INotificationRepository _notificationRepository, ILogger<UpdateNotificationCommandHandler> _logger) : IRequestHandler<UpdateNotificationCommand, Result>
{
    public async Task<Result> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            NotificationId = request.NotificationId,
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            ContentRu = request.ContentRu,
            ContentKk = request.ContentKk,
            ContentEn = request.ContentEn,
            NotificationTypeId = request.NotificationTypeId,
        };
        var result = await _notificationRepository.UpdateAsync(notification);
        if(result.IsFailed) 
        {
            _logger.LogError($"Ошибка при обновлении уведомления с id: {request.NotificationId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления уведомления."));
        }
        _logger.LogInformation($"Успешное обновление уведомления с id: {request.NotificationId}.");
        return result;
    }
}
