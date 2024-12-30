using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Queries.GetQueueTypeList;

public class GetQueueTypeListQueryHandler(IQueueTypeRepository queueTypeRepository, ILogger<GetQueueTypeListQueryHandler> logger) : IRequestHandler<GetQueueTypeListQuery, Result<List<QueueType>>>
{
    public async Task<Result<List<QueueType>>> Handle(GetQueueTypeListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка типов очередей из базы данных.");
        var result = await queueTypeRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка типов очередей из базы данных.", result.Error.Code);
            return Result.Failure<List<QueueType>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success(result.Value);
    }
}
