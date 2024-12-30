using AutoMapper;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Notifications.Queries.GetNotificationById;

public class GetNotificationByIdQueryHandler(INotificationRepository _notificationRepository, ILogger<GetNotificationByIdQueryHandler> logger) :
    IRequestHandler<GetNotificationByIdQuery, Result>
{
    public async Task<Result> Handle(GetNotificationByIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение уведомления  из базы данных.");
        var result = await _notificationRepository.GetNotificationdById(request.NotificationId);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение уведомления из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
