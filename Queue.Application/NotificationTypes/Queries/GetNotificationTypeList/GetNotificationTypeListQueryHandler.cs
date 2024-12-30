using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeList;

public class GetNotificationTypeListQueryHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<GetNotificationTypeListQueryHandler> logger) :
    IRequestHandler<GetNotificationTypeListQuery, Result<List<NotificationType>>>
{
    public async Task<Result<List<NotificationType>>> Handle(GetNotificationTypeListQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка типа уведомлений из базы данных.");
        var result = await _notificationTypeRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка типа уведомлений из базы данных.", result.Error.Code);
            return Result.Failure<List<NotificationType>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }

}
