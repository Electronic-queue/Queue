using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using Serilog.Core;

namespace Queue.Application.NotificationTypes.Commands.CreateNotificationType;

public class CreateNotificationTypeCommandHandler(INotificationTypeRepository _notificationTypeRepository, ILogger<CreateNotificationTypeCommandHandler> _logger) : IRequestHandler<CreateNotificationTypeCommand, Result>
{
    public async Task<Result> Handle(CreateNotificationTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на создание нового типа уведомления в базе данных.");
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
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового типа уведомления в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}