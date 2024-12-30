using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Services.Queries.GetServiceList;

public class GetServiceListQueryHandler(IServiceRepository _ServiceRepository, ILogger<GetServiceListQueryHandler> logger) :
    IRequestHandler<GetServiceListQuery, Result<List<Service>>>
{
    public async Task<Result<List<Service>>> Handle(GetServiceListQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка услуг из базы данных.");
        var result = await _ServiceRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка услуг из базы данных.", result.Error.Code);
            return Result.Failure<List<Service>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }

}
