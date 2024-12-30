using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Notifications.Queries.GetNotificationList;

public class GetNotificationListQueryHandler(INotificationRepository _notificationRepository, ILogger<GetNotificationListQueryHandler> logger) :
    IRequestHandler<GetNotificationListQuery, Result<List<Notification>>>
{
    public async Task<Result<List<Notification>>> Handle(GetNotificationListQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка действий из базы данных.");
        var result = await _notificationRepository.GetAllAsync();
        if (result.IsFailed)
        {

            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка действий из базы данных.", result.Error.Code);
            return Result.Failure<List<Notification>>(result.Error);
        }
        var notifications = result.Value;
        return notifications;
    }

}
