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
        _logger.LogInformation("Запрос на обновление услуги");
        var service = await _serviceRepository.UpdateAsync(
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

        if (service.IsFailed)
        {
            _logger.LogError($"Ошибка при обновлении услуги с id: {request.ServiceId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления услуги."));
        }
        _logger.LogInformation($"Успешное обновление услуги с id: {request.ServiceId}.");
        return Result.Success();
    }
}
