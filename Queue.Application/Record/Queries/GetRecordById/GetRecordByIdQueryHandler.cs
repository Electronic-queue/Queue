using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.Record.Queries.GetRecordById;

public class GetRecordByIdQueryHandler(IRecordRepository recordRepository, ILogger<GetRecordByIdQueryHandler> logger) :IRequestHandler<GetRecordByIdQuery,Result>
{
    public async Task<Result> Handle (GetRecordByIdQuery request,CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение записи из базы данных.");
        var result= await recordRepository.GetRecordById(request.RecordId);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение записи из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
