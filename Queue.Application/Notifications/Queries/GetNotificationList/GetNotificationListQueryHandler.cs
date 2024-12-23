using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Notifications.Queries.GetNotificationList;

public class GetNotificationListQueryHandler(INotificationRepository _notificationRepository) :
    IRequestHandler<GetNotificationListQuery, Result<List<Notification>>>
{
    public async Task<Result<List<Notification>>> Handle(GetNotificationListQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _notificationRepository.GetAllAsync();
        if (result == null || result.IsFailed)
        {
            return (Result<List<Notification>>)Result.Failure(new Error(Errors.NotAllowed, "Ошибка при запросе списка уведомлений."));
        }
        var notifications = result.Value;
        return notifications;
    }

}
