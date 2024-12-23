using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;


namespace Queue.Application.NotificationTypes.Commands.UpdateNotificationType;

public class UpdateNotificationTypeCommandHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<UpdateNotificationTypeCommandHandler> _logger) : IRequestHandler<UpdateNotificationTypeCommand, Result>
{
    public async Task<Result> Handle(UpdateNotificationTypeCommand request, CancellationToken cancellationToken)
    {
        var notificationType = new NotificationType
        {
            NotificationTypeId = request.NotificationTypeId,
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
        };
        var result = await _notificationTypeRepository.UpdateAsync(notificationType);
        if(result.IsFailed) 
        {
            _logger.LogError($"Ошибка при обновлении типа уведомлений с id: {request.NotificationTypeId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления типа уведомлений."));
        }
        _logger.LogInformation($"Успешное обновление типа уведомления с id: {request.NotificationTypeId}.");
        return result;
    }
}
