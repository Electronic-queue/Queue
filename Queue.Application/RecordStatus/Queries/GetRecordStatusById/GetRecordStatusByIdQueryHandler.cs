using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.RecordStatus.Queries.GetRecordStatusById;

public class GetRecordStatusByIdQueryHandler(IRecordStatusRepository recordStatusRepository, ILogger<GetRecordStatusByIdQueryHandler> logger) :IRequestHandler<GetRecordStatusByIdQuery,Result>
{
    public async Task<Result> Handle(GetRecordStatusByIdQuery request,CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение статуса записи из базы данных.");
        var result = await recordStatusRepository.GetRecordStatusById(request.RecordStatusId);
        if (result.IsFailed) 
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение статуса записи из базы данных.", result.Error.Code);
            return Result.Failure<Action>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
