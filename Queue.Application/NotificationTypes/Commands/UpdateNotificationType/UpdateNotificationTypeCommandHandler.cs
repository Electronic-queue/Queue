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
        _logger.LogInformation("Запрос на обновление типа уведомления");
        var notificationType = await _notificationTypeRepository.UpdateAsync(
            notificationTypeId:request.NotificationTypeId,
            nameRu:request.NameRu,
            nameKk:request.NameKk,
            nameEn:request.NameEn,
            descriptionRu:request.DescriptionRu,
            descriptionKk:request.DescriptionKk,
            descriptionEn:request.DescriptionEn
            );
       
        if(notificationType.IsFailed) 
        {
            _logger.LogError($"Ошибка при обновлении типа уведомлений с id: {request.NotificationTypeId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления типа уведомлений."));
        }
        _logger.LogInformation($"Успешное обновление типа уведомления с id: {request.NotificationTypeId}.");
        return Result.Success();
    }
}
