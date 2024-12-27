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
        _logger.LogInformation("Запрос на обновление уведомления");
        var notification = await _notificationRepository.UpdateAsync(
            notificationId:request.NotificationId,
            nameRu:request.NameRu,
            nameKk:request.NameKk,
            nameEn:request.NameEn,
            contentRu:request.ContentRu,
            contentKk:request.ContentKk,
            contentEn:request.ContentEn,
            notificationTypeId:request.NotificationTypeId
            );
        
        if(notification.IsFailed) 
        {
            _logger.LogError($"Ошибка при обновлении уведомления с id: {request.NotificationId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления уведомления."));
        }
        _logger.LogInformation($"Успешное обновление уведомления с id: {request.NotificationId}.");
        return Result.Success();
    }
}
