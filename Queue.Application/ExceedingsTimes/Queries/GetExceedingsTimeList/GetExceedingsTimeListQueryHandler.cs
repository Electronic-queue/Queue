using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeList;

public class GetExceedingsTimeListQueryHandler(IExceedingsTimeRepository timeRepository, ILogger<GetExceedingsTimeListQueryHandler> logger) : IRequestHandler<GetExceedingsTimeListQuery, Result<List<ExceedingsTime>>>
{
    public async Task<Result<List<ExceedingsTime>>> Handle(GetExceedingsTimeListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка времени перерыва из базы данных.");
        var result = await timeRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка времени перерыва из базы данных.", result.Error.Code);
            return Result.Failure<List<ExceedingsTime>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
