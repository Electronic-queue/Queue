using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Services.Commands.CreateService;

public class CreateServiceCommandHandler(IServiceRepository _ServiceRepository, ILogger<CreateServiceCommandHandler> _logger) : IRequestHandler<CreateServiceCommand, Result>
{
    public async Task<Result> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            AverageExecutionTime = request.AverageExecutionTime,
            QueueTypeId = request.QueueTypeId,
            ParentserviceId = request.ParentserviceId,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result = await _ServiceRepository.AddAsync(service);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при создании услуги.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка создания услуги."));
        }
        _logger.LogInformation($"Успешное создание услуги.");
        return result;
    }
}