using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;


namespace Queue.Application.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler(IServiceRepository _serviceRepository, ILogger<UpdateServiceCommandHandler> _logger) : IRequestHandler<UpdateServiceCommand, Result>
{
    public async Task<Result> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            ServiceId = request.ServiceId,
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            AverageExecutionTime = request.AverageExecutionTime,
            QueueTypeId = request.QueueTypeId,
            ParentserviceId = request.ParentserviceId,
        };
        var result = await _serviceRepository.UpdateAsync(service);
        if(result.IsFailed) 
        {
            _logger.LogError($"Ошибка при обновлении услуги с id: {request.ServiceId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления услуги."));
        }
        _logger.LogInformation($"Успешное обновление услуги с id: {request.ServiceId}.");
        return result;
    }
}
