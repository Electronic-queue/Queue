using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;


namespace Queue.Application.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler(IServiceRepository _serviceRepository, ILogger<UpdateServiceCommandHandler> _logger) : IRequestHandler<UpdateServiceCommand, Result>
{
    public async Task<Result> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление услуги в базе данных.");
        var result = await _serviceRepository.UpdateAsync(
            serviceId: request.ServiceId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            descriptionRu: request.DescriptionRu,
            descriptionKk: request.DescriptionKk,
            descriptionEn: request.DescriptionEn,
            avarageExecutionTime: request.AverageExecutionTime,
            queueTypeId: request.QueueTypeId,
            parentServiceId: request.ParentserviceId
            );

        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление услуги в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
