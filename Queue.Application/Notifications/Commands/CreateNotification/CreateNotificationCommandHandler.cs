using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Notifications.Commands.CreateNotification;

public class CreateNotificationCommandHandler(INotificationRepository _notificationRepository, ILogger<CreateNotificationCommandHandler> _logger) : IRequestHandler<CreateNotificationCommand, Result>
{
    public async Task<Result> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            ContentRu = request.ContentRu,
            ContentKk = request.ContentKk,
            ContentEn = request.ContentEn,
            NotificationTypeId = request.NotificationTypeId,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result = await _notificationRepository.AddAsync(notification);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при создании уведомления.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка создания уведомления."));
        }
        _logger.LogInformation($"Успешное создание уведомления.");
        return Result.Success();
    }
}