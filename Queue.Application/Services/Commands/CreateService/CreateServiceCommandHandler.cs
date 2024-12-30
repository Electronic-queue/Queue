using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Services.Commands.CreateService;

public class CreateServiceCommandHandler(IServiceRepository _ServiceRepository, ILogger<CreateServiceCommandHandler> _logger) : IRequestHandler<CreateServiceCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на создание новой услуги в базе данных.");
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
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result = await _ServiceRepository.AddAsync(service);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание новой услуги в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}