using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Queries.GetQueueTypeById;

public class GetQueueTypeByIdQueryHandler(IQueueTypeRepository queueTypeRepository, ILogger<GetQueueTypeByIdQueryHandler> logger) :IRequestHandler<GetQueueTypeByIdQuery, Result>
{
    public async Task<Result> Handle(GetQueueTypeByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение типа очереди из базы данных.");
        var result = await queueTypeRepository.GetQueueTypedById(request.QueueTypeId);
        if(result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение типа очереди из базы данных.", result.Error.Code);
            return Result.Failure<Action>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
