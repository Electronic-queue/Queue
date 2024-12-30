using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;

public class GetNotificationTypeDetailsQueryHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<GetNotificationTypeDetailsQueryHandler> logger) :
    IRequestHandler<GetNotificationTypeDetailsQuery, Result>
{
    public async Task<Result> Handle(GetNotificationTypeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение типа уведомления из базы данных.");
        var result = await _notificationTypeRepository.GetNotificationTypeById(request.NotificationTypeId);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение типа уведомления из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
