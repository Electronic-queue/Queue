using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;

public class GetNotificationTypeDetailsQueryHandler(INotificationTypeRepository _notificationTypeRepository):
    IRequestHandler<GetNotificationTypeDetailsQuery, Result>
{
    public async Task<Result> Handle(GetNotificationTypeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _notificationTypeRepository.GetNotificationTypeById(request.NotificationTypeId);
        if (result == null || result.IsFailed)
        {
            return Result.Failure(new Error(Errors.NotAllowed, "Ошибка при запросе типа уведомления."));
        }
        return result;
    }
}
