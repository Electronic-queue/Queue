using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ReasonsForCancellations.Queries.GetReasonsForCancellationList;

public class GetReasonsForCancellationListQueryHandler(IReasonsForCancellationRepository reasonRepository, ILogger<GetReasonsForCancellationListQueryHandler> logger) : IRequestHandler<GetReasonsForCancellationListQuery, Result<List<ReasonsForCancellation>>>
{
    public async Task<Result<List<ReasonsForCancellation>>> Handle(GetReasonsForCancellationListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка причин для отмен из базы данных.");
        var result = await reasonRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка причин для отмен из базы данных.", result.Error.Code);
            return (Result<List<ReasonsForCancellation>>)Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
