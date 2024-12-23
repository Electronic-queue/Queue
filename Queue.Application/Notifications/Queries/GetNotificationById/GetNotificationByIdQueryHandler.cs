using AutoMapper;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Notifications.Queries.GetNotificationById;

public class GetServiceByIdQueryHandler(INotificationRepository _notificationRepository):
    IRequestHandler<GetNotificationByIdQuery, Result<Notification>>
{
    public async Task<Result<Notification>> Handle(GetNotificationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _notificationRepository.GetNotificationById(request.NotificationId);
        if (result.IsFailed)
        {
            return Result.Failure<Notification>(new Error(Errors.NotAllowed, "Ошибка при запросе уведомления."));
        }
        return result;
    }
}
