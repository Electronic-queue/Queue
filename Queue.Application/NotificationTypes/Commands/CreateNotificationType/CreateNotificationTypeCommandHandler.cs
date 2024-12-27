using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.NotificationTypes.Commands.CreateNotificationType;

public class CreateNotificationTypeCommandHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<CreateNotificationTypeCommandHandler> _logger) : IRequestHandler<CreateNotificationTypeCommand, Result>
{
    public async Task<Result> Handle(CreateNotificationTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание типа уведомления");
        var notificationType = new NotificationType
        {
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result = await _notificationTypeRepository.AddAsync(notificationType);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при создании типа уведомлений.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка создания типа уведомлений."));
        }
        _logger.LogInformation($"Успешное создание типа уведомления.");
        return result;
    }
}