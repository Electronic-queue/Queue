using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using Serilog.Core;


namespace Queue.Application.NotificationTypes.Commands.UpdateNotificationType;

public class UpdateNotificationTypeCommandHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<UpdateNotificationTypeCommandHandler> _logger) : IRequestHandler<UpdateNotificationTypeCommand, Result>
{
    public async Task<Result> Handle(UpdateNotificationTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление типа уведомления в базе данных.");
        var result = await _notificationTypeRepository.UpdateAsync(
            notificationTypeId:request.NotificationTypeId,
            nameRu:request.NameRu,
            nameKk:request.NameKk,
            nameEn:request.NameEn,
            descriptionRu:request.DescriptionRu,
            descriptionKk:request.DescriptionKk,
            descriptionEn:request.DescriptionEn
            );
       
        if(result.IsFailed) 
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление типа уведомления в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
