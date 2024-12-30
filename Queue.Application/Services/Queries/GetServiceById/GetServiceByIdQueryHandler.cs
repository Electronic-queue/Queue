using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler(IServiceRepository _serviceRepository, ILogger<GetServiceByIdQueryHandler> logger) :
    IRequestHandler<GetServiceByIdQuery, Result>
{
    public async Task<Result> Handle(GetServiceByIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение услуги из базы данных.");
        var result = await _serviceRepository.GetServiceById(request.ServiceId);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение услуги из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
