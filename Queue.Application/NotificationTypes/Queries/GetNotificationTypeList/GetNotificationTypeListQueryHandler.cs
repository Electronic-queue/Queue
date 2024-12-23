using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeList;

public class GetNotificationTypeListQueryHandler(INotificationTypeRepository _notificationTypeRepository) :
    IRequestHandler<GetNotificationTypeListQuery, Result<List<NotificationType>>>
{
    public async Task<Result<List<NotificationType>>> Handle(GetNotificationTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _notificationTypeRepository.GetAllAsync();
        if (result.IsFailed)
        {
            return (Result<List<NotificationType>>)Result.Failure(new Error(Errors.NotAllowed, "Ошибка при запросе списка типов уведомлений."));
        }
        var notificationTypes = result.Value;
        return result;
    }

}
